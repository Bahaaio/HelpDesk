using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Authorization.Requirements;
using Workbench.Modules.Issues.Models;

namespace Workbench.Modules.Authorization.Handlers;

public class AssignedOrLeadHandler : AuthorizationHandler<AssignedOrLeadRequirement, Issue>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AssignedOrLeadRequirement requirement, Issue resource)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
