using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;

namespace Workbench.Modules.Authorization.Handlers;

public class OwnerOrTeamMemberHandler : AuthorizationHandler<OwnerOrTeamMemberRequirement, IBelongsToProject>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OwnerOrTeamMemberRequirement requirement, IBelongsToProject resource)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
