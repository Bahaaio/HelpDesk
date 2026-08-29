using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;

namespace Workbench.Modules.Authorization.Handlers;

public class OwnerOrLeadHandler : AuthorizationHandler<OwnerOrLeadRequirement, IBelongsToProject>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OwnerOrLeadRequirement requirement, IBelongsToProject resource)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
