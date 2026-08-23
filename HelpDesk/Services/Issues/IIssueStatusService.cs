using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;

namespace HelpDesk.Services.Issues;

/// <summary>
///     Manages issue status changes and history.
/// </summary>
public interface IIssueStatusService
{
    /// <summary>
    ///     Updates the status of a issue. Only the issue author or a technician may change status.
    /// </summary>
    /// <param name="issueId">The issue ID.</param>
    /// <param name="request">The new status value.</param>
    Task UpdateStatus(int issueId, UpdateIssueStatusRequest request);

    /// <summary>
    ///     Returns the status change history for a issue.
    /// </summary>
    /// <param name="issueId">The issue ID.</param>
    Task<List<StatusChangeDto>> GetStatusHistory(int issueId);
}
