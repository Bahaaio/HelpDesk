using HelpDesk.Modules.Authorization.Models;
using HelpDesk.Modules.Authorization.Requirements;
using HelpDesk.Modules.Authorization.Services;

namespace HelpDesk.Modules.Authorization.Extensions;

public static class AuthorizationGuardExtensions
{
    extension(IAuthorizationGuard authorizationGuard)
    {
        /// <summary>
        ///     Authorizes the current user as either the owner of the resource or a technician.
        /// </summary>
        /// <param name="resource">The resource to authorize against.</param>
        public Task AuthorizeOwnerOrTechnician(IOwnedByUser resource) =>
            authorizationGuard.Authorize(resource, new OwnerOrTechnicianRequirement());
    }
}