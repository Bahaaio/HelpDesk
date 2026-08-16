using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketStatusHistoryService _ticketStatusHistoryService;
    private readonly ITicketsService _ticketsService;

    public TicketsController(ITicketsService ticketsService,
        ITicketStatusHistoryService ticketStatusHistoryService)
    {
        _ticketsService = ticketsService;
        _ticketStatusHistoryService = ticketStatusHistoryService;
    }

    [HttpGet]
    public async Task<ActionResult<TicketDto>> GetAll([FromQuery] TicketQuery query) =>
        Ok(await _ticketsService.GetAll(query));

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetById(int id) =>
        Ok(await _ticketsService.GetById(id));

    [HttpPost]
    public async Task<ActionResult> Create(CreateTicketRequest request)
    {
        var ticket = await _ticketsService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateTicketRequest request) =>
        Ok(await _ticketsService.Update(id, request));

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult> UpdateStatus(int id, UpdateTicketStatusRequest request)
    {
        await _ticketsService.UpdateStatus(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _ticketsService.Delete(id);
        return NoContent();
    }

    [HttpGet("{id:int}/status-history")]
    public async Task<ActionResult<List<StatusChangeDto>>> GetStatusHistory(int id) =>
        Ok(await _ticketStatusHistoryService.GetStatusHistory(id));
}