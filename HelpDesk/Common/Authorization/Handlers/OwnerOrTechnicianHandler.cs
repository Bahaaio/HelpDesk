using HelpDesk.Common.Authorization.Requirements;
using HelpDesk.Modules.Auth.Enums;
using HelpDesk.Modules.Auth.Services;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Common.Authorization.Handlers;

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