namespace Workbench.Modules.Issues.Services;

/// <summary>
///     Manages the tags associated with a specific issue.
/// </summary>
public interface IIssueTagsService
{
    /// <summary>
    ///     Replaces all tags on a issue with the specified tag names. Requires technician role.
    /// </summary>
    /// <param name="issueId">The ID of the issue to update tags for.</param>
    /// <param name="tags">The list of tag names to assign to the issue.</param>
    Task<List<string>> UpdateTags(int issueId, List<string> tags);
}
