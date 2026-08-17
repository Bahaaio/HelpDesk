using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Extensions;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

/// <summary>
///     Generic attachment service.
/// </summary>
/// <remarks>
///     This class does not implement any authorization logic.
///     You must implement authorization logic in the derived classes.
/// </remarks>
/// <typeparam name="T">The type of resource to attach to.</typeparam>
public abstract class AttachmentsService<T> : IAttachmentsService where T : Attachment
{
    private readonly IAttachmentValidationService _attachmentValidationService;
    private readonly AppDbContext _db;
    private readonly ILogger<AttachmentsService<T>> _logger;
    private readonly IStorageService _storageService;
    private readonly ICurrentUser _user;

    public AttachmentsService(IStorageService storageService, AppDbContext db, ICurrentUser user,
        ILogger<AttachmentsService<T>> logger,
        IAttachmentValidationService attachmentValidationService)
    {
        _storageService = storageService;
        _db = db;
        _logger = logger;
        _attachmentValidationService = attachmentValidationService;
        _user = user;
    }

    public virtual async Task<AttachmentResult> Get(Guid attachmentId)
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

    public virtual async Task<AttachmentDto> Add(int resourceId, IFormFile file)
    {
        _attachmentValidationService.Validate(file);

        var count = await _db.Attachments.CountAsync(a => a.ResourceId == resourceId);
        _attachmentValidationService.ValidateCount(count + 1);

        var guid = Guid.NewGuid();
        await _storageService.Store(file, guid.ToString());

        var attachment = CreateAttachment(resourceId, file, _user.Id);
        attachment.Id = guid;

        _db.Attachments.Add(attachment);
        await _db.SaveChangesAsync();

        _logger.LogInformation(
            "User {userId} added attachment {attachmentId} to resource {ResourceId}",
            _user.Id, attachment.Id, resourceId);

        return new AttachmentDto(attachment.Id, file.ContentType, file.FileName);
    }

    public virtual async Task Delete(Guid attachmentId)
    {
        var attachment = await _db.Attachments.FindOrThrowAsync(attachmentId);

        _db.Remove(attachment);
        await _storageService.DeleteFile(attachmentId.ToString());
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} deleted attachment {attachmentId}",
            _user.Id, attachmentId);
    }

    public virtual async Task DeleteAll(int resourceId)
    {
        var attachmentIds = await _db.Attachments
            .Where(a => a.ResourceId == resourceId)
            .Select(a => a.Id.ToString())
            .ToListAsync();

        foreach (var id in attachmentIds)
            await _storageService.DeleteFile(id);

        await _db.Attachments
            .Where(a => a.ResourceId == resourceId)
            .ExecuteDeleteAsync();
    }

    protected abstract Attachment CreateAttachment(int resourceId, IFormFile file, int uploaderId);
}