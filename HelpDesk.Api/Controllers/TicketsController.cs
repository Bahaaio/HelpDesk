using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly TicketsService _ticketsService;

    public TicketsController(TicketsService ticketsService)
    {
        _ticketsService = ticketsService;
    }

    [HttpGet]
    public async Task<ActionResult<TicketDto>> GetAll([FromQuery] TicketQuery query)
    {
        return Ok(await _ticketsService.GetAll(query));
    }

    [HttpGet("mine")]
    public async Task<ActionResult<TicketDto>> GetMyTickets([FromQuery] TicketQuery query)
    {
        return Ok(await _ticketsService.GetCurrentUserTickets(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetById(int id)
    {
        var ticket = await _ticketsService.GetById(id);
        return ticket is null ? NotFound() : Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateTicketRequest request)
    {
        var ticket = await _ticketsService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateTicketRequest request)
    {
        return Ok(await _ticketsService.Update(id, request));
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult> UpdateStatus(int id, TicketStatusUpdateRequest request)
    {
        await _ticketsService.UpdateStatus(id, request);
        return NoContent();
    }
}