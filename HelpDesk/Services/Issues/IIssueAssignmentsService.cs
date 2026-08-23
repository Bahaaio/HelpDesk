using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;

namespace HelpDesk.Services.Issues;

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
    ///     Returns all issues assigned to the current user.
    /// </summary>
    /// <param name="issueQuery">Optional filters for status, tag, author, and free-text search.</param>
    Task<List<IssueDto>> GetCurrentUserAssignedIssues(IssueQuery issueQuery);
}
