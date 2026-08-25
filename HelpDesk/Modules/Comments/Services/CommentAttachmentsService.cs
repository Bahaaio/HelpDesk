using HelpDesk.Common.Authorization;
using HelpDesk.Common.Extensions;
using HelpDesk.Data;
using HelpDesk.Modules.Attachments;
using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Options;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Comments.Models;
using HelpDesk.Modules.Comments.Options;
using HelpDesk.Modules.Storage.Services;
using Microsoft.Extensions.Options;

namespace HelpDesk.Modules.Comments.Services;

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

    public override async Task<AttachmentDto> Add(int parentId, IFormFile file)
    {
        var comment = await GetOwnerEntity(parentId);
        await _authGuard.AuthorizeOwnerOrTechnician(comment);

        return await base.Add(parentId, file);
    }

    public override async Task Delete(Guid attachmentId)
    {
        var comment = await GetOwnerEntity(attachmentId);
        await _authGuard.AuthorizeOwnerOrTechnician(comment);

        await base.Delete(attachmentId);
    }
}