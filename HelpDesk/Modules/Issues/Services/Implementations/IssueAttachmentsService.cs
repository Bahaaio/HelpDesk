using HelpDesk.Common.Authorization;
using HelpDesk.Common.Extensions;
using HelpDesk.Data;
using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Options;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Attachments.Services.Implementations;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Issues.Models;
using HelpDesk.Modules.Issues.Options;
using HelpDesk.Modules.Storage.Services;
using Microsoft.Extensions.Options;

namespace HelpDesk.Modules.Issues.Services.Implementations;

public class IssueAttachmentsService : AttachmentsService<Issue, IssueAttachment>
{
    private readonly IAuthorizationGuard _authGuard;

    public IssueAttachmentsService(IStorageService storageService, AppDbContext db,
        ICurrentUser user, ILogger<IssueAttachmentsService> logger,
        IAttachmentValidationService attachmentValidationService,
        IAuthorizationGuard authGuard, IOptions<IssueAttachmentOptions> attachmentOptions)
        : base(storageService, db, user, logger, attachmentValidationService)
    {
        _authGuard = authGuard;
        AttachmentOptions = attachmentOptions.Value;
    }

    protected override AttachmentOptions AttachmentOptions { get; }

    public override async Task<AttachmentDto> Add(int parentId, IFormFile file)
    {
        var issue = await GetOwnerEntity(parentId);
        await _authGuard.AuthorizeOwnerOrTechnician(issue);

        return await base.Add(parentId, file);
    }

    public override async Task Delete(Guid attachmentId)
    {
        var issue = await GetOwnerEntity(attachmentId);
        await _authGuard.AuthorizeOwnerOrTechnician(issue);

        await base.Delete(attachmentId);
    }
}