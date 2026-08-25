using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Models;

namespace HelpDesk.Modules.Issues.Repositories;

public interface IIssueStatusChangeRepository : IRepository<IssueStatusChange, int>
{
    /// <summary>Returns the ordered status-change history for the given issue.</summary>
    Task<List<StatusChangeDto>> GetHistoryAsync(int issueId);
}
