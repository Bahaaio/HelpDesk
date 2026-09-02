using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;
using Workbench.Modules.Projects.Memberships.Repositories;

namespace Workbench.Modules.Authorization.Handlers;

public class TeamMemberHandler : AuthorizationHandler<TeamMemberRequirement, IBelongsToProject>
{
    private readonly IProjectMembershipsRepository _membershipsRepository;
    private readonly ICurrentUser _user;

    public TeamMemberHandler(IProjectMembershipsRepository membershipsRepository, ICurrentUser user)
    {
        _membershipsRepository = membershipsRepository;
        _user = user;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        TeamMemberRequirement requirement,
        IBelongsToProject resource)
    {
        var membership = await _membershipsRepository
            .FindMembershipByProjectIdAndUserId(resource.ProjectId, _user.Id);

        if (membership is not null)
            context.Succeed(requirement);
    }
}