using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;

namespace Workbench.Modules.Authorization.Handlers;

public class OwnerHandler : AuthorizationHandler<OwnerRequirement, IOwnedByUser>
{
    private readonly ICurrentUser _user;

    public OwnerHandler(ICurrentUser user)
    {
        _user = user;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerRequirement requirement,
        IOwnedByUser resource)
    {
        if (resource.OwnerId == _user.Id)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}