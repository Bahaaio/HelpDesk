using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Extensions;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class AttachmentsService : IAttachmentsService
{
    private readonly IAttachmentValidationService _attachmentValidationService;
    private readonly IAuthorizationGuard _authGuard;
    private readonly AppDbContext _db;
    private readonly ILogger<AttachmentsService> _logger;
    private readonly IStorageService _storageService;
    private readonly ICurrentUser _user;

    public AttachmentsService(IStorageService storageService, AppDbContext db, ICurrentUser user,
        IAuthorizationGuard authGuard, ILogger<AttachmentsService> logger,
        IAttachmentValidationService attachmentValidationService)
    {
        _storageService = storageService;
        _db = db;
        _authGuard = authGuard;
        _logger = logger;
        _attachmentValidationService = attachmentValidationService;
        _user = user;
    }

    public async Task<AttachmentDto> AddAttachment(int ticketId, IFormFile file)
    {
        var ticket = await _db.Tickets.FindOrThrowAsync(ticketId);

        await _authGuard.AuthorizeOwnerOrTechnician(ticket);

        _attachmentValidationService.Validate(file);

        var count = await _db.Attachments.CountAsync(a => a.TicketId == ticketId);
        _attachmentValidationService.ValidateCount(count + 1);

        var guid = Guid.NewGuid();
        await _storageService.Store(file, guid.ToString());

        var attachment = new Attachment
        {
            Id = guid,
            ContentType = file.ContentType,
            OriginalFileName = file.FileName,
            TicketId = ticketId,
            UploaderId = _user.Id
        };

        _db.Attachments.Add(attachment);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} added attachment {attachmentId} to ticket {ticketId}",
            _user.Id, attachment.Id, ticketId);

        return new AttachmentDto(attachment.Id, file.ContentType, file.FileName);
    }

    public async Task DeleteAttachment(Guid attachmentId)
    {
        var attachment = await _db.Attachments.FindOrThrowAsync(attachmentId);

        await _authGuard.AuthorizeOwnerOrTechnician(attachment);

        _db.Remove(attachment);
        await _storageService.DeleteFile(attachmentId.ToString());
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} deleted attachment {attachmentId}",
            _user.Id, attachmentId);
    }

    public async Task<AttachmentResult> GetAttachment(Guid attachmentId)
    {
        var stream = await _storageService.Load(attachmentId.ToString());

        var attachment = await _db.Attachments
            .Where(a => a.Id == attachmentId)
            .Select(a => new { a.ContentType, a.OriginalFileName })
            .SingleOrDefaultAsync();

        if (stream is null || attachment is null)
            throw new NotFoundException($"Attachment with id: {attachmentId} not found");

        return new AttachmentResult(stream, attachment.ContentType, attachment.OriginalFileName);
    }

    public async Task DeleteAttachmentsForTicket(int ticketId)
    {
        var attachmentIds = await _db.Attachments
            .Where(a => a.TicketId == ticketId)
            .Select(a => a.Id.ToString())
            .ToListAsync();

        foreach (var id in attachmentIds)
            await _storageService.DeleteFile(id);

        await _db.Attachments
            .Where(a => a.TicketId == ticketId)
            .ExecuteDeleteAsync();
    }
}