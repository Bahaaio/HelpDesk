using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Issues.Services;

namespace Workbench.Modules.Issues.Controllers;

[ApiController]
[Route("api/issues/{issueId:int}/tags")]
public class IssueTagsController : ControllerBase
{
    private readonly IIssueTagsService _issueTagsService;

    public IssueTagsController(IIssueTagsService issueTagsService)
    {
        _issueTagsService = issueTagsService;
    }

    [HttpPut]
    public async Task<ActionResult<List<string>>> UpdateTags(int issueId, List<string> tags) =>
        Ok(await _issueTagsService.UpdateTags(issueId, tags));
}