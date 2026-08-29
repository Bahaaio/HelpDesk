using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Issues.Dtos.Requests;
using Workbench.Modules.Issues.Services;

namespace Workbench.Modules.Issues.Controllers;

[ApiController]
[Route("api/issues/mine")]
public class CurrentUserIssuesController : ControllerBase
{
    private readonly IIssueAssignmentsService _issueAssignmentsService;
    private readonly IIssuesService _issuesService;

    public CurrentUserIssuesController(IIssuesService issuesService,
        IIssueAssignmentsService issueAssignmentsService)
    {
        _issuesService = issuesService;
        _issueAssignmentsService = issueAssignmentsService;
    }

    [HttpGet]
    public async Task<ActionResult<IssueDto>> GetMyIssues([FromQuery] IssueQuery query) =>
        Ok(await _issuesService.GetCurrentUserIssues(query));

    [HttpGet("assigned")]
    public async Task<ActionResult<IssueDto>> GetAssignedIssues([FromQuery] IssueQuery query) =>
        Ok(await _issueAssignmentsService.GetCurrentUserAssignedIssues(query));
}