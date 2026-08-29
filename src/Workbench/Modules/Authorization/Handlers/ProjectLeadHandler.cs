using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;

namespace Workbench.Modules.Authorization.Handlers;

public class ProjectLeadHandler : AuthorizationHandler<ProjectLeadRequirement, IBelongsToProject>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        ProjectLeadRequirement requirement, IBelongsToProject resource)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}