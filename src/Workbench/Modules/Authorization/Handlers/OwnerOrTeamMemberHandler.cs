using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;
using Workbench.Modules.Projects.Memberships.Repositories;

namespace Workbench.Modules.Authorization.Handlers;

public class OwnerOrTeamMemberHandler :
    AuthorizationHandler<OwnerOrTeamMemberRequirement, IBelongsToProject>
{
    private readonly IProjectMembershipsRepository _membershipsRepository;
    private readonly ICurrentUser _user;

    public OwnerOrTeamMemberHandler(ICurrentUser user,
        IProjectMembershipsRepository membershipsRepository)
    {
        _user = user;
        _membershipsRepository = membershipsRepository;
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

        var membership = await _membershipsRepository
            .FindMembershipByProjectIdAndUserId(resource.ProjectId, _user.Id);

        if (membership is not null)
            context.Succeed(requirement);
    }
}