using HelpDesk.Api.Authorization.Requirements;
using HelpDesk.Api.Models;
using HelpDesk.Api.Models.Enums;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Api.Authorization.Handlers;

public class AttachmentUploaderOrTechnicianHandler :
    AuthorizationHandler<AttachmentUploaderOrTechnicianRequirement, Attachment>
{
    private readonly ICurrentUser _user;

    public AttachmentUploaderOrTechnicianHandler(ICurrentUser user)
    {
        _user = user;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AttachmentUploaderOrTechnicianRequirement requirement,
        Attachment resource)
    {
        if (resource.UploaderId == _user.Id || _user.Role == Role.Technician)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}