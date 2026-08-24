using HelpDesk.Data;
using HelpDesk.Dtos.Responses;
using HelpDesk.Extensions;
using HelpDesk.Models;
using HelpDesk.Options;
using HelpDesk.Services.Attachments;
using HelpDesk.Services.Auth;
using HelpDesk.Services.Storage;
using Microsoft.Extensions.Options;

namespace HelpDesk.Services.Issues;

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