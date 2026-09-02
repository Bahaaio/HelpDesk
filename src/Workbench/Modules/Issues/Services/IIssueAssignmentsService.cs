using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Issues.Dtos.Requests;

namespace Workbench.Modules.Issues.Services;

public interface IIssueAssignmentsService
{
    /// <summary>
    ///     Assigns the current user to the issue.
    /// </summary>
    /// <param name="issueId">The ID of the issue to assign.</param>
    Task AssignCurrentUser(int issueId);

    /// <summary>
    ///     Unassigns the current user from the issue.
    /// </summary>
    /// <param name="issueId">The ID of the issue to unassign.</param>
    Task UnassignCurrentUser(int issueId);

    /// <summary>
    ///     Assigns a user to the issue.
    /// </summary>
    /// <param name="issueId">The ID of the issue to assign.</param>
    /// <param name="userName">The username of the user to assign.</param>
    Task AssignUser(int issueId, string userName);

    /// <summary>
    ///     Unassigns a user from the issue.
    /// </summary>
    /// <param name="issueId">The ID of the issue to unassign.</param>
    Task UnassignUser(int issueId);

    /// <summary>
    ///     Returns all issues assigned to the current user.
    /// </summary>
    /// <param name="issueQuery">Optional filters for status, tag, author, and free-text search.</param>
    Task<List<IssueDto>> GetCurrentUserAssignedIssues(IssueQuery issueQuery);
}