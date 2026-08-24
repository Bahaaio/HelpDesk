using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Comments.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Modules.Comments.Controllers;

[ApiController]
[Route("api/comments/{commentId:int}/attachments")]
public class CommentAttachmentsController : ControllerBase
{
    private readonly IAttachmentsService<Comment> _attachmentsService;

    public CommentAttachmentsController(IAttachmentsService<Comment> attachmentsService)
    {
        _attachmentsService = attachmentsService;
    }

    [HttpPost]
    public async Task<ActionResult<AttachmentDto>> Attach(int commentId, IFormFile file) =>
        Ok(await _attachmentsService.Add(commentId, file));

    [HttpDelete("{AttachmentId:guid}")]
    public async Task<ActionResult> Delete(int commentId, Guid attachmentId)
    {
        await _attachmentsService.Delete(attachmentId);
        return NoContent();
    }
}