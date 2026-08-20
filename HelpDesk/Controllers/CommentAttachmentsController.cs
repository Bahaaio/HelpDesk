using HelpDesk.Dtos.Responses;
using HelpDesk.Models;
using HelpDesk.Services.Attachments;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

[ApiController]
[Route("api/comments/{commentId:int}/attachments")]
public class CommentAttachmentsController : ControllerBase
{
    private readonly IAttachmentsService<Comment> _attachmentsService;

    public CommentAttachmentsController(IAttachmentsService<Comment> attachmentsService)
    {
        _attachmentsService = attachmentsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AttachmentDto>>> GetAll(int commentId) =>
        Ok(await _attachmentsService.GetAll(commentId));

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