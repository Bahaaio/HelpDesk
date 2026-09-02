using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Issues.Services;

namespace Workbench.Modules.Issues.Controllers;

[ApiController]
[Route("api/issues/{issueId:int}/assignments")]
public class IssueAssignmentsController : ControllerBase
{
    private readonly IIssueAssignmentsService _issueAssignmentsService;

    public IssueAssignmentsController(IIssueAssignmentsService issueAssignmentsService)
    {
        _issueAssignmentsService = issueAssignmentsService;
    }

    [HttpPost]
    public async Task<ActionResult> AssignCurrentUser(int issueId)
    {
        await _issueAssignmentsService.AssignCurrentUser(issueId);
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> UnassignCurrentUser(int issueId)
    {
        await _issueAssignmentsService.UnassignCurrentUser(issueId);
        return NoContent();
    }

    [HttpPost("{username}")]
    public async Task<ActionResult> AssignUser(int issueId, string username)
    {
        await _issueAssignmentsService.AssignUser(issueId, username);
        return NoContent();
    }

    [HttpDelete("all")]
    public async Task<ActionResult> UnassignUser(int issueId)
    {
        await _issueAssignmentsService.UnassignUser(issueId);
        return NoContent();
    }
}
