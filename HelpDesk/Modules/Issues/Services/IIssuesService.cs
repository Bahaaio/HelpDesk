using HelpDesk.Common.Exceptions;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Dtos.Requests;

namespace HelpDesk.Modules.Issues.Services;

/// <summary>
///     Manages IT helpdesk issues including CRUD, status changes, and filtering.
/// </summary>
public interface IIssuesService
{
    /// <summary>
    ///     Returns all issues matching the specified filters.
    /// </summary>
    /// <param name="issueQuery">Optional filters for status, tag, author, and free-text search.</param>
    Task<List<IssueDto>> GetAll(IssueQuery issueQuery);

    /// <summary>
    ///     Returns issues created by the current user, matching the specified filters.
    /// </summary>
    /// <param name="issueQuery">Optional filters for status, tag, author, and free-text search.</param>
    Task<List<IssueDto>> GetCurrentUserIssues(IssueQuery issueQuery);

    /// <summary>
    ///     Returns a single issue by its ID.
    /// </summary>
    /// <param name="id">The issue ID.</param>
    /// <exception cref="NotFoundException">Thrown if the issue does not exist.</exception>
    Task<IssueDto> GetById(int id);

    /// <summary>
    ///     Creates a new issue assigned to the current user.
    /// </summary>
    /// <param name="request">The issue title and optional description.</param>
    Task<IssueDto> Create(CreateIssueRequest request);

    /// <summary>
    ///     Updates an existing issue. Only the issue author or a technician may update.
    /// </summary>
    /// <param name="id">The issue ID.</param>
    /// <param name="request">The updated title and optional description.</param>
    Task<IssueDto> Update(int id, UpdateIssueRequest request);

    /// <summary>
    ///     Deletes a issue and its attachments from storage. Only the issue author or a technician may
    ///     delete.
    /// </summary>
    /// <param name="id">The issue ID.</param>
    Task Delete(int id);
}