using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Services.Issues;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly IIssuesService _issuesService;

    public IssuesController(IIssuesService issuesService)
    {
        _issuesService = issuesService;
    }

    [HttpGet]
    public async Task<ActionResult<IssueDto>> GetAll([FromQuery] IssueQuery query) =>
        Ok(await _issuesService.GetAll(query));

    [HttpGet("{id}")]
    public async Task<ActionResult<IssueDto>> GetById(int id) =>
        Ok(await _issuesService.GetById(id));

    [HttpPost]
    public async Task<ActionResult> Create(CreateIssueRequest request)
    {
        var issue = await _issuesService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = issue.Id }, issue);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateIssueRequest request) =>
        Ok(await _issuesService.Update(id, request));

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _issuesService.Delete(id);
        return NoContent();
    }
}
