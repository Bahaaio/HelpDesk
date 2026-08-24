using HelpDesk.Authorization;
using HelpDesk.Data;
using HelpDesk.Dtos.Responses;
using HelpDesk.Exceptions;
using HelpDesk.Extensions;
using HelpDesk.Mappers;
using HelpDesk.Models;
using HelpDesk.Options;
using HelpDesk.Services.Auth;
using HelpDesk.Services.Storage;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Attachments;

/// <summary>
///     Generic attachment service.
/// </summary>
/// <remarks>
///     This class does not implement any authorization logic.
///     You must implement authorization logic in the derived classes.
/// </remarks>
/// <typeparam name="TOwner">The owner of the attachment.</typeparam>
/// <typeparam name="TAttachment">The attachment type.</typeparam>
public abstract class AttachmentsService<TOwner, TAttachment> : IAttachmentsService<TOwner>
    where TOwner : class, IOwnedByUser, IEntity<int>
    where TAttachment : Attachment, new()
{
    private readonly IAttachmentValidationService _attachmentValidationService;
    private readonly DbSet<TAttachment> _attachmentsSet;
    private readonly AppDbContext _db;
    private readonly ILogger<AttachmentsService<TOwner, TAttachment>> _logger;
    private readonly DbSet<TOwner> _ownerSet;
    private readonly IStorageService _storageService;
    private readonly ICurrentUser _user;

    protected AttachmentsService(IStorageService storageService, AppDbContext db, ICurrentUser user,
        ILogger<AttachmentsService<TOwner, TAttachment>> logger,
        IAttachmentValidationService attachmentValidationService)
    {
        _storageService = storageService;
        _db = db;
        _logger = logger;
        _attachmentValidationService = attachmentValidationService;
        _ownerSet = db.Set<TOwner>();
        _user = user;
        _attachmentsSet = db.Set<TAttachment>();
    }

    protected abstract AttachmentOptions AttachmentOptions { get; }

    public async Task<List<AttachmentDto>> GetAll(int ownerId)
    {
        return await _attachmentsSet
            .Where(a => a.OwnerId == ownerId)
            .Select(a => a.ToDto())
            .ToListAsync();
    }

    public virtual async Task<AttachmentDto> Add(int ownerId, IFormFile file)
    {
        _attachmentValidationService.Validate(file, AttachmentOptions);
        await _ownerSet.ExistsOrThrowAsync(ownerId);

        var count = await _attachmentsSet.CountAsync(a => a.OwnerId == ownerId);
        _attachmentValidationService.ValidateCount(count + 1, AttachmentOptions.MaxCount);

        var guid = Guid.NewGuid();
        await _storageService.Store(file, guid.ToString());

        var attachment = new TAttachment
        {
            Id = guid,
            OwnerId = ownerId,
            ContentType = file.ContentType,
            OriginalFileName = file.FileName,
            UploaderId = _user.Id
        };

        _db.Attachments.Add(attachment);
        await _db.SaveChangesAsync();

        _logger.LogInformation(
            "User {userId} added attachment {attachmentId} to owner {OwnerId}",
            _user.Id, attachment.Id, ownerId);

        return attachment.ToDto();
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
        var attachmentIds = await _attachmentsSet
            .Where(a => a.OwnerId == ownerId)
            .Select(a => a.Id.ToString())
            .ToListAsync();

        foreach (var id in attachmentIds)
            await _storageService.DeleteFile(id);

        await _attachmentsSet
            .Where(a => a.OwnerId == ownerId)
            .ExecuteDeleteAsync();
    }

    /// <summary>
    ///     Gets the owner entity of the attachment.
    ///     Used to implement authorization logic.
    /// </summary>
    /// <param name="attachmentId">The ID of the attachment.</param>
    /// <returns>The owner entity.</returns>
    /// <exception cref="NotFoundException">Thrown if the attachment is not found.</exception>
    protected async Task<TOwner> GetOwnerEntity(Guid attachmentId)
    {
        return await _ownerSet.Where(o =>
                   o.Id == _attachmentsSet
                       .Where(a => a.Id == attachmentId)
                       .Select(a => a.OwnerId)
                       .FirstOrDefault()
               ).SingleOrDefaultAsync() ??
               throw new NotFoundException($"Attachment with id: {attachmentId} not found");
    }

    /// <summary>
    ///     Gets the owner entity of the attachment.
    /// </summary>
    /// <param name="ownerId">The ID of the owner.</param>
    /// <returns>The owner entity.</returns>
    protected async Task<TOwner> GetOwnerEntity(int ownerId) =>
        await _ownerSet.FindOrThrowAsync(ownerId);
}