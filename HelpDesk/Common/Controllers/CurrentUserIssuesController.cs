using HelpDesk.Modules.Auth.Enums;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Dtos.Requests;
using HelpDesk.Modules.Issues.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Common.Controllers;

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