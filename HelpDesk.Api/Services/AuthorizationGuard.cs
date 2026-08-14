using HelpDesk.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Api.Services;

public class AuthorizationGuard : IAuthorizationGuard
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUser _user;

    public AuthorizationGuard(IAuthorizationService authorizationService, ICurrentUser user)
    {
        _authorizationService = authorizationService;
        _user = user;
    }

    public async Task Authorize(object resource, IAuthorizationRequirement requirement)
    {
        var result = await _authorizationService.AuthorizeAsync(
            _user.Principal,
            resource,
            requirement
        );

        if (!result.Succeeded)
            throw new ForbiddenException("You are not authorized to perform this action");
    }
}