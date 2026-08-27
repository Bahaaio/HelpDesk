using Workbench.Modules.Auth.Enums;
using Workbench.Modules.Issues.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Workbench.Modules.Issues.Controllers;

[Authorize(Roles = Role.Technician)]
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
}
