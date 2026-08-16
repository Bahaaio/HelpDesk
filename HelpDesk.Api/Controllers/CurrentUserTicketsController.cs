using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Models.Enums;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

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