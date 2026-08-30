using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;
using Workbench.Modules.Projects.Memberships.Services;

namespace Workbench.Modules.Authorization.Handlers;

public class TeamMemberHandler : AuthorizationHandler<TeamMemberRequirement, IBelongsToProject>
{
    private readonly IProjectMembershipsService _membershipsService;

    public TeamMemberHandler(IProjectMembershipsService membershipsService)
    {
        _membershipsService = membershipsService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        TeamMemberRequirement requirement,
        IBelongsToProject resource)
    {
        var membership = await _membershipsService
            .GetCurrentUserProjectMembership(resource.ProjectId);

        if (membership is not null)
            context.Succeed(requirement);
    }
}