using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;
using Workbench.Modules.Projects.Enums;
using Workbench.Modules.Projects.Memberships.Services;

namespace Workbench.Modules.Authorization.Handlers;

public class OwnerOrLeadHandler : AuthorizationHandler<OwnerOrLeadRequirement, IBelongsToProject>
{
    private readonly IProjectMembershipsService _membershipsService;
    private readonly ICurrentUser _user;

    public OwnerOrLeadHandler(IProjectMembershipsService membershipsService, ICurrentUser user)
    {
        _membershipsService = membershipsService;
        _user = user;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerOrLeadRequirement requirement,
        IBelongsToProject resource)
    {
        if (resource is IOwnedByUser ownedResource && ownedResource.OwnerId == _user.Id)
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