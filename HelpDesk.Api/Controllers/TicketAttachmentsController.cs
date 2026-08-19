using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Models;
using HelpDesk.Api.Services.Attachments;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/tickets/{ticketId:int}/attachments")]
public class TicketAttachmentsController : ControllerBase
{
    private readonly IAttachmentsService<Ticket> _attachmentsService;

    public TicketAttachmentsController(IAttachmentsService<Ticket> attachmentsService)
    {
        _attachmentsService = attachmentsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AttachmentDto>>> GetAll(int ticketId) =>
        Ok(await _attachmentsService.GetAll(ticketId));

    [HttpPost]
    public async Task<ActionResult<AttachmentDto>> Attach(int ticketId, IFormFile file) =>
        Ok(await _attachmentsService.Add(ticketId, file));

    [HttpDelete("{AttachmentId:guid}")]
    public async Task<ActionResult> Delete(int ticketId, Guid attachmentId)
    {
        await _attachmentsService.Delete(attachmentId);
        return NoContent();
    }
}