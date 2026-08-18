using HelpDesk.Api.Models.Enums;
using HelpDesk.Api.Services.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[Authorize(Roles = Role.Technician)]
[ApiController]
[Route("api/tickets/{ticketId:int}/assignments")]
public class TicketAssignmentsController : ControllerBase
{
    private readonly ITicketAssignmentsService _ticketAssignmentsService;

    public TicketAssignmentsController(ITicketAssignmentsService ticketAssignmentsService)
    {
        _ticketAssignmentsService = ticketAssignmentsService;
    }

    [HttpPost]
    public async Task<ActionResult> AssignCurrentUser(int ticketId)
    {
        await _ticketAssignmentsService.AssignCurrentUser(ticketId);
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> UnassignCurrentUser(int ticketId)
    {
        await _ticketAssignmentsService.UnassignCurrentUser(ticketId);
        return NoContent();
    }
}