using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Options;
using HelpDesk.Modules.Attachments.Repositories;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Attachments.Services.Implementations;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Authorization.Extensions;
using HelpDesk.Modules.Authorization.Services;
using HelpDesk.Modules.Comments.Models;
using HelpDesk.Modules.Comments.Options;
using HelpDesk.Modules.Comments.Repositories;
using HelpDesk.Modules.Storage.Services;
using Microsoft.Extensions.Options;

namespace HelpDesk.Modules.Comments.Services.Implementations;

public class CommentAttachmentsService : AttachmentsService<Comment, CommentAttachment>
{
    private readonly IAuthorizationGuard _authGuard;

    public CommentAttachmentsService(
        IStorageService storageService,
        ICommentsRepository commentsRepository,
        IAttachmentsRepository<CommentAttachment> attachmentsRepository,
        IUnitOfWork unitOfWork,
        ICurrentUser user,
        ILogger<AttachmentsService<Comment, CommentAttachment>> logger,
        IAttachmentValidationService attachmentValidationService,
        IOptions<CommentAttachmentOptions> attachmentOptions,
        IAuthorizationGuard authGuard)
        : base(storageService, commentsRepository, attachmentsRepository, unitOfWork, user, logger,
            attachmentValidationService)
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