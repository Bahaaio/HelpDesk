using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/tickets/{ticketId:int}/attachments")]
public class TicketAttachmentsController : ControllerBase
{
    private readonly AttachmentsService _attachmentsService;

    public TicketAttachmentsController(AttachmentsService attachmentsService)
    {
        _attachmentsService = attachmentsService;
    }

    [HttpPost]
    public async Task<ActionResult> Attach(int ticketId, IFormFile file)
    {
        if (file.ContentType != "image/jpeg")
            throw new BadRequestException("Only jpeg files are allowed");

        return Ok(await _attachmentsService.AddAttachment(ticketId, file, User));
    }

    [HttpDelete("{AttachmentId:guid}")]
    public async Task<ActionResult> Delete(int ticketId, Guid attachmentId)
    {
        await _attachmentsService.DeleteAttachment(attachmentId, User);
        return NoContent();
    }
}