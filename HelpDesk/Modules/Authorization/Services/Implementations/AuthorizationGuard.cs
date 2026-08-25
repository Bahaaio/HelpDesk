using HelpDesk.Common.Exceptions;
using HelpDesk.Modules.Auth.Services;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Modules.Authorization.Services.Implementations;

public class AuthorizationGuard : IAuthorizationGuard
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ILogger<AuthorizationGuard> _logger;
    private readonly ICurrentUser _user;

    public AuthorizationGuard(IAuthorizationService authorizationService, ICurrentUser user,
        ILogger<AuthorizationGuard> log)
    {
        _authorizationService = authorizationService;
        _user = user;
        _logger = log;
    }

    public async Task Authorize(object resource, IAuthorizationRequirement requirement)
    {
        var result = await _authorizationService.AuthorizeAsync(
            _user.Principal,
            resource,
            requirement
        );

        if (!result.Succeeded)
        {
            _logger.LogWarning("User {userId} is not authorized for {Requirement} on {resource}",
                _user.Id, requirement.GetType().Name, resource.GetType().Name);

            throw new ForbiddenException("You are not authorized to perform this action");
        }
    }
}