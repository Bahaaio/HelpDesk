using HelpDesk.Data;
using HelpDesk.Dtos.Responses;
using HelpDesk.Extensions;
using HelpDesk.Models;
using HelpDesk.Options;
using HelpDesk.Services.Attachments;
using HelpDesk.Services.Auth;
using HelpDesk.Services.Storage;
using Microsoft.Extensions.Options;

namespace HelpDesk.Services.Comments;

public class CommentAttachmentsService : AttachmentsService<Comment, CommentAttachment>
{
    private readonly IAuthorizationGuard _authGuard;

    public CommentAttachmentsService(IStorageService storageService, AppDbContext db,
        ICurrentUser user, ILogger<AttachmentsService<Comment, CommentAttachment>> logger,
        IAttachmentValidationService attachmentValidationService,
        IOptions<CommentAttachmentOptions> attachmentOptions, IAuthorizationGuard authGuard)
        : base(storageService, db, user, logger, attachmentValidationService)
    {
        _authGuard = authGuard;
        AttachmentOptions = attachmentOptions.Value;
    }

    protected override AttachmentOptions AttachmentOptions { get; }

    public override async Task<AttachmentDto> Add(int ownerId, IFormFile file)
    {
        var comment = await GetOwnerEntity(ownerId);
        await _authGuard.AuthorizeOwnerOrTechnician(comment);

        return await base.Add(ownerId, file);
    }

    public override async Task Delete(Guid attachmentId)
    {
        var comment = await GetOwnerEntity(attachmentId);
        await _authGuard.AuthorizeOwnerOrTechnician(comment);

        await base.Delete(attachmentId);
    }
}