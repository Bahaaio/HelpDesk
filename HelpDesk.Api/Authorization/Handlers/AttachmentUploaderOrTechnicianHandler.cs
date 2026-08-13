using System.Security.Claims;
using HelpDesk.Api.Authorization.Requirements;
using HelpDesk.Api.Models;
using HelpDesk.Api.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Api.Authorization.Handlers;

public class AttachmentUploaderOrTechnicianHandler :
    AuthorizationHandler<AttachmentUploaderOrTechnicianRequirement, Attachment>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AttachmentUploaderOrTechnicianRequirement requirement,
        Attachment resource)
    {
        var userId = int.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (resource.UploaderId == userId
            || context.User.IsInRole(Role.Technician))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}