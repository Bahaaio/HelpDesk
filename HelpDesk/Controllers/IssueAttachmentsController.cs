using HelpDesk.Dtos.Responses;
using HelpDesk.Models;
using HelpDesk.Services.Attachments;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

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