using Workbench.Modules.Projects.Invites.Dtos;
using Workbench.Modules.Projects.Invites.Dtos.Requests;

namespace Workbench.Modules.Projects.Invites.Services;

/// <summary>
///     Manages technician invite codes. Handles creation, validation, and consumption.
/// </summary>
public interface IProjectInvitesService
{
    /// <summary>
    ///     Creates a new invite code valid for the specified duration.
    /// </summary>
    /// <param name="request">The invite configuration including validity duration.</param>
    Task<InviteDto> Create(CreateInviteRequest request);

    /// <summary>
    ///     Retrieves all active invite codes for a specific project.
    /// </summary>
    /// <param name="projectId">The ID of the project for which to retrieve active invite codes.</param>
    /// <returns>A list of active invite codes associated with the specified project.</returns>
    Task<List<InviteDto>> GetActive(int projectId);

    /// <summary>
    ///     Validates an invitation code and deletes it.
    ///     Adds the current user to the project if the code is valid.
    /// </summary>
    /// <param name="code">The invite code to validate and consume.</param>
    Task Consume(string code);

    /// <summary>
    ///     Revokes an existing invite code, making it invalid for future use.
    /// </summary>
    /// <param name="code">The invite code to revoke.</param>
    Task Revoke(string code);
}