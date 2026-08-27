using Workbench.Modules.Auth.Enums;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace Workbench.Modules.Authorization.Handlers;

public class OwnerOrTechnicianHandler :
    AuthorizationHandler<OwnerOrTechnicianRequirement, IOwnedByUser>
{
    private readonly ICurrentUser _user;

    public OwnerOrTechnicianHandler(ICurrentUser user)
    {
        _user = user;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerOrTechnicianRequirement requirement,
        IOwnedByUser resource)
    {
        if (resource.OwnerId == _user.Id || _user.Role == Role.Technician)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
