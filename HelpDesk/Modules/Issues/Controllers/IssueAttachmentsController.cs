using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Issues.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Modules.Issues.Controllers;

[ApiController]
[Route("api/issues/{issueId:int}/attachments")]
public class IssueAttachmentsController : ControllerBase
{
    private readonly IAttachmentsService<Issue> _attachmentsService;

    public IssueAttachmentsController(IAttachmentsService<Issue> attachmentsService)
    {
        _attachmentsService = attachmentsService;
    }

    [HttpPost]
    public async Task<ActionResult<AttachmentDto>> Attach(int issueId, IFormFile file) =>
        Ok(await _attachmentsService.Add(issueId, file));

    [HttpDelete("{AttachmentId:guid}")]
    public async Task<ActionResult> Delete(int issueId, Guid attachmentId)
    {
        await _attachmentsService.Delete(attachmentId);
        return NoContent();
    }
}