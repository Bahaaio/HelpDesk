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
/// <typeparam name="TJoin">The join table between attachments and owner.</typeparam>
public abstract class AttachmentsService<TOwner, TJoin> : IAttachmentsService<TOwner>
    where TOwner : class, IOwnedByUser, IEntity<int>
    where TJoin : class, IAttachmentJoin<TOwner>, new()
{
    private readonly DbSet<TJoin> _attachmentJoinSet;
    private readonly IAttachmentValidationService _attachmentValidationService;
    private readonly AppDbContext _db;
    private readonly ILogger<AttachmentsService<TOwner, TJoin>> _logger;
    private readonly DbSet<TOwner> _ownerSet;
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
        _ownerSet = db.Set<TOwner>();
        _user = user;
        _attachmentJoinSet = db.Set<TJoin>();
    }

    protected abstract AttachmentOptions AttachmentOptions { get; }

    public async Task<List<AttachmentDto>> GetAll(int ownerId)
    {
        return await _attachmentJoinSet
            .Where(aj => aj.OwnerId == ownerId)
            .Select(aj => aj.Attachment.ToDto())
            .ToListAsync();
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

    /// <summary>
    ///     Gets the owner entity of the attachment.
    ///     Used to implement authorization logic.
    /// </summary>
    /// <param name="attachmentId">The ID of the attachment.</param>
    /// <returns>The owner entity.</returns>
    /// <exception cref="NotFoundException">Thrown if the attachment is not found.</exception>
    protected async Task<TOwner> GetOwnerEntity(Guid attachmentId)
    {
        var attachmentJoin = await _attachmentJoinSet
            .Where(aj => aj.AttachmentId == attachmentId)
            .SingleOrDefaultAsync();

        if (attachmentJoin is null)
            throw new NotFoundException($"Attachment with id {attachmentId} not found");

        return await _ownerSet.FindOrThrowAsync(attachmentJoin.OwnerId);
    }

    /// <summary>
    ///     Gets the owner entity of the attachment.
    /// </summary>
    /// <param name="ownerId">The ID of the owner.</param>
    /// <returns>The owner entity.</returns>
    protected async Task<TOwner> GetOwnerEntity(int ownerId) =>
        await _ownerSet.FindOrThrowAsync(ownerId);
}