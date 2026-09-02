using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Requirements;
using Workbench.Modules.Issues.Models;
using Workbench.Modules.Projects.Enums;
using Workbench.Modules.Projects.Memberships.Repositories;

namespace Workbench.Modules.Authorization.Handlers;

public class AssignedOrLeadHandler : AuthorizationHandler<AssignedOrLeadRequirement, Issue>
{
    private readonly IProjectMembershipsRepository _membershipsRepository;
    private readonly ICurrentUser _user;

    public AssignedOrLeadHandler(ICurrentUser user,
        IProjectMembershipsRepository membershipsRepository)
    {
        _user = user;
        _membershipsRepository = membershipsRepository;
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

        var membership = await _membershipsRepository
            .FindMembershipByProjectIdAndUserId(resource.ProjectId, _user.Id);

        if (membership?.Role == ProjectMemberRole.Lead)
            context.Succeed(requirement);
    }
}