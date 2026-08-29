using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Milestones.Services;

namespace Workbench.Modules.Milestones.Controllers;

[ApiController]
[Route("api/projects/{projectId:int}/milestones/{milestoneId:int}/issues")]
public class MilestoneIssuesController : ControllerBase
{
    private readonly IMilestoneIssuesService _milestoneIssuesService;

    public MilestoneIssuesController(IMilestoneIssuesService milestoneIssuesService)
    {
        _milestoneIssuesService = milestoneIssuesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<IssueDto>>> GetAllIssues(int projectId, int milestoneId) =>
        Ok(await _milestoneIssuesService.GetAllIssues(projectId, milestoneId));

    [HttpPost("{issueId}")]
    public async Task<IActionResult> AddIssue(int projectId, int milestoneId, int issueId)
    {
        await _milestoneIssuesService.AddIssue(projectId, milestoneId, issueId);
        return NoContent();
    }

    [HttpDelete("{issueId}")]
    public async Task<IActionResult> RemoveIssue(int projectId, int milestoneId, int issueId)
    {
        await _milestoneIssuesService.RemoveIssue(projectId, milestoneId, issueId);
        return NoContent();
    }
}
