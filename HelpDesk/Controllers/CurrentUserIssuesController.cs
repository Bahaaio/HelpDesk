using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Models.Enums;
using HelpDesk.Services.Issues;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

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

    [Authorize(Roles = Role.Technician)]
    [HttpGet("assigned")]
    public async Task<ActionResult<IssueDto>> GetAssignedIssues([FromQuery] IssueQuery query) =>
        Ok(await _issueAssignmentsService.GetCurrentUserAssignedIssues(query));
}
