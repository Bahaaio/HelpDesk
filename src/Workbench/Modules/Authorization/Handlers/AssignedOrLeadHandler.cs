using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Requirements;
using Workbench.Modules.Issues.Models;
using Workbench.Modules.Projects.Enums;
using Workbench.Modules.Projects.Memberships.Services;

namespace Workbench.Modules.Authorization.Handlers;

public class AssignedOrLeadHandler : AuthorizationHandler<AssignedOrLeadRequirement, Issue>
{
    private readonly IProjectMembershipsService _membershipsService;
    private readonly ICurrentUser _user;

    public AssignedOrLeadHandler(ICurrentUser user, IProjectMembershipsService membershipsService)
    {
        _user = user;
        _membershipsService = membershipsService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AssignedOrLeadRequirement requirement,
        Issue resource)
    {
        if (resource.AssignedToId == _user.Id)
        {
            context.Succeed(requirement);
            return;
        }

        var membership = await _membershipsService
            .GetCurrentUserProjectMembership(resource.ProjectId);

        if (membership?.Role == ProjectMemberRole.Lead)
            context.Succeed(requirement);
    }
}