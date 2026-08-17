using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Extensions;
using HelpDesk.Api.Models;
using HelpDesk.Api.Options;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

/// <summary>
///     Generic attachment service.
/// </summary>
/// <remarks>
///     This class does not implement any authorization logic.
///     You must implement authorization logic in the derived classes.
/// </remarks>
/// <typeparam name="TOwner">The owner of the attachment.</typeparam>
/// <typeparam name="TJoin">The join table between attachments and owner.</typeparam>
public abstract class AttachmentsService<TOwner, TJoin> : IAttachmentsService
    where TJoin : class, IAttachmentJoin<TOwner>, new()
{
    private readonly DbSet<TJoin> _attachmentJoinSet;
    private readonly IAttachmentValidationService _attachmentValidationService;
    private readonly AppDbContext _db;
    private readonly ILogger<AttachmentsService<TOwner, TJoin>> _logger;
    private readonly IStorageService _storageService;
    private readonly ICurrentUser _user;

    protected AttachmentsService(IStorageService storageService, AppDbContext db, ICurrentUser user,
        ILogger<AttachmentsService<TOwner, TJoin>> logger,
        IAttachmentValidationService attachmentValidationService)
    {
        _storageService = storageService;
        _db = db;
        _logger = logger;
        _attachmentValidationService = attachmentValidationService;
        _user = user;
        _attachmentJoinSet = db.Set<TJoin>();
    }

    protected abstract AttachmentOptions AttachmentOptions { get; }

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

    public virtual async Task<AttachmentDto> Add(int ownerId, IFormFile file)
    {
        _attachmentValidationService.Validate(file, AttachmentOptions);

        var count = await _attachmentJoinSet.CountAsync(aj => aj.OwnerId == ownerId);
        _attachmentValidationService.ValidateCount(count + 1, AttachmentOptions.MaxCount);

        var guid = Guid.NewGuid();
        await _storageService.Store(file, guid.ToString());

        var attachment = new Attachment
        {
            Id = guid,
            ContentType = file.ContentType,
            OriginalFileName = file.FileName,
            UploaderId = _user.Id
        };
        _db.Attachments.Add(attachment);

        _attachmentJoinSet.Add(new TJoin
        {
            AttachmentId = attachment.Id,
            OwnerId = ownerId
        });

        await _db.SaveChangesAsync();

        _logger.LogInformation(
            "User {userId} added attachment {attachmentId} to owner {OwnerId}",
            _user.Id, attachment.Id, ownerId);

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

    public virtual async Task DeleteAll(int ownerId)
    {
        var attachmentIds = await _attachmentJoinSet
            .Where(aj => aj.OwnerId == ownerId)
            .Select(aj => aj.AttachmentId.ToString())
            .ToListAsync();

        foreach (var id in attachmentIds)
            await _storageService.DeleteFile(id);

        await _attachmentJoinSet
            .Where(aj => aj.OwnerId == ownerId)
            .ExecuteDeleteAsync();
    }
}