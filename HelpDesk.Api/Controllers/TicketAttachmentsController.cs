using HelpDesk.Api.Services.Attachments;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/tickets/{ticketId:int}/attachments")]
public class TicketAttachmentsController : ControllerBase
{
    private readonly IAttachmentsService _attachmentsService;

    public TicketAttachmentsController(IAttachmentsService attachmentsService)
    {
        _attachmentsService = attachmentsService;
    }

    [HttpPost]
    public async Task<ActionResult> Attach(int ticketId, IFormFile file) =>
        Ok(await _attachmentsService.Add(ticketId, file));

    [HttpDelete("{AttachmentId:guid}")]
    public async Task<ActionResult> Delete(int ticketId, Guid attachmentId)
    {
        await _attachmentsService.Delete(attachmentId);
        return NoContent();
    }
}