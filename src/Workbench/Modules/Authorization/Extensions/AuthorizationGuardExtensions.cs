using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Authorization.Requirements;
using Workbench.Modules.Authorization.Services;
using Workbench.Modules.Issues.Models;

namespace Workbench.Modules.Authorization.Extensions;

/// <summary>
///     Extension methods for the <see cref="IAuthorizationGuard" /> interface.
/// </summary>
public static class AuthorizationGuardExtensions
{
    extension(IAuthorizationGuard authorizationGuard)
    {
        /// <summary>
        ///     Authorizes the current user as the owner of the resource.
        /// </summary>
        /// <param name="resource">The resource to authorize against.</param>
        public Task AuthorizeOwner(IOwnedByUser resource) =>
            authorizationGuard.Authorize(resource, new OwnerRequirement());

        /// <summary>
        ///     Authorizes the current user as either the owner of the resource or a lead.
        /// </summary>
        /// <param name="resource">The resource to authorize against.</param>
        /// <typeparam name="TResource">The type of the resource.</typeparam>
        public Task AuthorizeOwnerOrProjectLead<TResource>(TResource resource)
            where TResource : IBelongsToProject, IOwnedByUser =>
            authorizationGuard.Authorize(resource, new OwnerOrLeadRequirement());

        /// <summary>
        ///     Authorizes the current user as a team member of the project or the owner of the resource.
        /// </summary>
        /// <param name="resource"></param>
        /// <typeparam name="TResource">The type of the resource.</typeparam>
        public Task AuthorizeOwnerOrProjectMember<TResource>(TResource resource)
            where TResource : IBelongsToProject, IOwnedByUser =>
            authorizationGuard.Authorize(resource, new OwnerOrTeamMemberRequirement());

        /// <summary>
        ///     Authorizes the current user as a team member of the project.
        /// </summary>
        /// <param name="resource">The resource to authorize against.</param>
        public Task AuthorizeProjectMember(IBelongsToProject resource) =>
            authorizationGuard.Authorize(resource, new TeamMemberRequirement());

        /// <summary>
        ///     Authorizes the current user as either the assigned user of the issue or a lead of the project.
        /// </summary>
        /// <param name="resource">The issue resource to authorize against.</param>
        public Task AuthorizeAssignedOrProjectLead(Issue resource) =>
            authorizationGuard.Authorize(resource, new AssignedOrLeadRequirement());
    }
}