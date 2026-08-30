using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;
using Workbench.Modules.Projects.Enums;
using Workbench.Modules.Projects.Memberships.Services;

namespace Workbench.Modules.Authorization.Handlers;

public class ProjectLeadHandler : AuthorizationHandler<ProjectLeadRequirement, IBelongsToProject>
{
    private readonly IProjectMembershipsService _membershipsService;

    public ProjectLeadHandler(IProjectMembershipsService membershipsService)
    {
        _membershipsService = membershipsService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ProjectLeadRequirement requirement,
        IBelongsToProject resource)
    {
        var membership = await _membershipsService
            .GetCurrentUserProjectMembership(resource.ProjectId);

        if (membership?.Role == ProjectMemberRole.Lead)
            context.Succeed(requirement);
    }
}