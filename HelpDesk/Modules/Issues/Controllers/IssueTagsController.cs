using HelpDesk.Modules.Auth.Enums;
using HelpDesk.Modules.Issues.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Modules.Issues.Controllers;

[Authorize(Roles = Role.Technician)]
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