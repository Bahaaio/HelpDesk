using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;
using Workbench.Modules.Projects.Enums;
using Workbench.Modules.Projects.Memberships.Repositories;

namespace Workbench.Modules.Authorization.Handlers;

public class ProjectLeadHandler : AuthorizationHandler<ProjectLeadRequirement, IBelongsToProject>
{
    private readonly IProjectMembershipsRepository _membershipsRepository;
    private readonly ICurrentUser _user;

    public ProjectLeadHandler(IProjectMembershipsRepository membershipsRepository,
        ICurrentUser user)
    {
        _membershipsRepository = membershipsRepository;
        _user = user;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ProjectLeadRequirement requirement,
        IBelongsToProject resource)
    {
        var membership = await _membershipsRepository
            .FindMembershipByProjectIdAndUserId(resource.ProjectId, _user.Id);

        if (membership?.Role == ProjectMemberRole.Lead)
            context.Succeed(requirement);
    }
}