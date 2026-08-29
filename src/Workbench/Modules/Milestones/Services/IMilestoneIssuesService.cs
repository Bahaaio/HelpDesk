using Workbench.Modules.Issues.Dtos;

namespace Workbench.Modules.Milestones.Services;

public interface IMilestoneIssuesService
{
    /// <summary>Returns all issues in a milestone.</summary>
    Task<List<IssueDto>> GetAllIssues(int projectId, int milestoneId);

    /// <summary>Adds an issue to a milestone.</summary>
    Task AddIssue(int projectId, int milestoneId, int issueId);

    /// <summary>Removes an issue from a milestone.</summary>
    Task RemoveIssue(int projectId, int milestoneId, int issueId);
}
