using Workbench.Data.Persistence;
using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Issues.Models;

namespace Workbench.Modules.Issues.Repositories;

public interface IIssueStatusChangeRepository : IRepository<IssueStatusChange, int>
{
    /// <summary>Returns the ordered status-change history for the given issue.</summary>
    Task<List<StatusChangeDto>> GetHistoryAsync(int issueId);
}
