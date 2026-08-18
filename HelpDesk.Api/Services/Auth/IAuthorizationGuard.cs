using HelpDesk.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Api.Services.Auth;

public interface IAuthorizationGuard
{
    /// <summary>
    ///     Ensures that the current user is authorized to perform the specified action on the specified
    ///     resource.
    /// </summary>
    /// <exception cref="ForbiddenException">
    ///     Thrown when the user is not authorized to perform the action
    /// </exception>
    /// <param name="resource">the resource to authorize against</param>
    /// <param name="requirement">the requirement to authorize against</param>
    Task Authorize(object resource, IAuthorizationRequirement requirement);
}