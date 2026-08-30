using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;
using Workbench.Modules.Projects.Memberships.Services;

namespace Workbench.Modules.Authorization.Handlers;

public class OwnerOrTeamMemberHandler :
    AuthorizationHandler<OwnerOrTeamMemberRequirement, IBelongsToProject>
{
    private readonly IProjectMembershipsService _membershipsService;
    private readonly ICurrentUser _user;

    public OwnerOrTeamMemberHandler(ICurrentUser user,
        IProjectMembershipsService membershipsService)
    {
        _user = user;
        _membershipsService = membershipsService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerOrTeamMemberRequirement requirement,
        IBelongsToProject resource)
    {
        if (resource is IOwnedByUser ownedResource && ownedResource.OwnerId == _user.Id)
        {
            context.Succeed(requirement);
            return;
        }

        var membership = await _membershipsService
            .GetCurrentUserProjectMembership(resource.ProjectId);

        if (membership is not null)
            context.Succeed(requirement);
    }
}