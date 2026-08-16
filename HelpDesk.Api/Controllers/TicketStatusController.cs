using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/tickets/{ticketId:int}/status")]
public class TicketStatusController : ControllerBase
{
    private readonly ITicketStatusService _ticketStatusService;

    public TicketStatusController(ITicketStatusService ticketStatusService)
    {
        _ticketStatusService = ticketStatusService;
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateStatus(int ticketId, UpdateTicketStatusRequest request)
    {
        await _ticketStatusService.UpdateStatus(ticketId, request);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<List<StatusChangeDto>>> GetStatusHistory(int ticketId) =>
        Ok(await _ticketStatusService.GetStatusHistory(ticketId));
}
