using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;

namespace Workbench.Modules.Authorization.Handlers;

public class OwnerHandler : AuthorizationHandler<OwnerRequirement, IOwnedByUser>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OwnerRequirement requirement, IOwnedByUser resource)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
