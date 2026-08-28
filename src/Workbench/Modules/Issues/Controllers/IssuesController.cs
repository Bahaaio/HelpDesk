using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Issues.Dtos.Requests;
using Workbench.Modules.Issues.Services;

namespace Workbench.Modules.Issues.Controllers;

[ApiController]
[Route("api/projects/{projectId:int}/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly IIssuesService _issuesService;

    public IssuesController(IIssuesService issuesService)
    {
        _issuesService = issuesService;
    }

    [HttpGet]
    public async Task<ActionResult<IssueDto>> GetAll(int projectId, [FromQuery] IssueQuery query) =>
        Ok(await _issuesService.GetAll(projectId, query));

    [HttpGet("{issueId}")]
    public async Task<ActionResult<IssueDto>> GetById(int projectId, int issueId) =>
        Ok(await _issuesService.GetById(projectId, issueId));

    [HttpPost]
    public async Task<ActionResult> Create(int projectId, CreateIssueRequest request)
    {
        var issue = await _issuesService.Create(projectId, request);
        return CreatedAtAction(nameof(GetById), new { projectId, issueId = issue.Id }, issue);
    }

    [HttpPut("{issueId}")]
    public async Task<ActionResult> Update(int projectId, int issueId, UpdateIssueRequest request)
        => Ok(await _issuesService.Update(projectId, issueId, request));

    [HttpDelete("{issueId}")]
    public async Task<ActionResult> Delete(int projectId, int issueId)
    {
        await _issuesService.Delete(projectId, issueId);
        return NoContent();
    }
}