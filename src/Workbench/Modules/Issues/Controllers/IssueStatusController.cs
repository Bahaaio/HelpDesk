using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Issues.Dtos.Requests;
using Workbench.Modules.Issues.Services;
using Microsoft.AspNetCore.Mvc;

namespace Workbench.Modules.Issues.Controllers;

[ApiController]
[Route("api/issues/{issueId:int}/status")]
public class IssueStatusController : ControllerBase
{
    private readonly IIssueStatusService _issueStatusService;

    public IssueStatusController(IIssueStatusService issueStatusService)
    {
        _issueStatusService = issueStatusService;
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateStatus(int issueId, UpdateIssueStatusRequest request)
    {
        await _issueStatusService.UpdateStatus(issueId, request);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<List<StatusChangeDto>>> GetStatusHistory(int issueId) =>
        Ok(await _issueStatusService.GetStatusHistory(issueId));
}
