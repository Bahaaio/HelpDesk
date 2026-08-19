using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Models.Enums;
using HelpDesk.Services.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

[ApiController]
[Route("api/tickets/mine")]
public class CurrentUserTicketsController : ControllerBase
{
    private readonly ITicketAssignmentsService _ticketAssignmentsService;
    private readonly ITicketsService _ticketsService;

    public CurrentUserTicketsController(ITicketsService ticketsService,
        ITicketAssignmentsService ticketAssignmentsService)
    {
        _ticketsService = ticketsService;
        _ticketAssignmentsService = ticketAssignmentsService;
    }

    [HttpGet]
    public async Task<ActionResult<TicketDto>> GetMyTickets([FromQuery] TicketQuery query) =>
        Ok(await _ticketsService.GetCurrentUserTickets(query));

    [Authorize(Roles = Role.Technician)]
    [HttpGet("assigned")]
    public async Task<ActionResult<TicketDto>> GetAssignedTickets([FromQuery] TicketQuery query) =>
        Ok(await _ticketAssignmentsService.GetCurrentUserAssignedTickets(query));
}
