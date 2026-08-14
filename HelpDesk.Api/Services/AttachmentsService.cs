using HelpDesk.Api.Authorization.Requirements;
using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Models;

namespace HelpDesk.Api.Services;

public class AttachmentsService : IAttachmentsService
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly AppDbContext _db;
    private readonly IStorageService _storageService;
    private readonly ICurrentUser _user;

    public AttachmentsService(IStorageService storageService, AppDbContext db, ICurrentUser user,
        IAuthorizationGuard authGuard)
    {
        _storageService = storageService;
        _db = db;
        _authGuard = authGuard;
        _user = user;
    }

    public async Task<AttachmentDto> AddAttachment(int ticketId, IFormFile file)
    {
        var ticket = await _db.Tickets.FindAsync(ticketId);

        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        await _authGuard.Authorize(ticket, new TicketOwnerOrTechnicianRequirement());

        var guid = Guid.NewGuid();

        await _storageService.Store(file, guid.ToString());

        var attachment = new Attachment
        {
            Id = guid,
            TicketId = ticketId,
            UploaderId = _user.Id
        };

        await _db.Attachments.AddAsync(attachment);
        await _db.SaveChangesAsync();

        return new AttachmentDto(attachment.Id);
    }

    public async Task DeleteAttachment(Guid attachmentId)
    {
        var attachment = await _db.Attachments.FindAsync(attachmentId);
        if (attachment is null)
            throw new NotFoundException($"Attachment with id: {attachmentId} not found");

        await _authGuard.Authorize(attachment, new AttachmentUploaderOrTechnicianRequirement());

        _db.Remove(attachment);
        await _storageService.DeleteFile(attachmentId.ToString());

        await _db.SaveChangesAsync();
    }

    public async Task<Stream> GetAttachment(Guid attachmentId)
    {
        var stream = await _storageService.Load(attachmentId.ToString());

        if (stream is null)
            throw new NotFoundException($"Attachment with id: {attachmentId} not found");

        return stream;
    }
}