using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController(TicketsService ticketsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<TicketDto>> GetAll()
    {
        return Ok(await ticketsService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetById(int id)
    {
        var ticket = await ticketsService.GetById(id);
        return ticket is null ? NotFound() : Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateTicketRequest request)
    {
        var ticket = await ticketsService.Create(request, User);
        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateTicketRequest request)
    {
        return Ok(await ticketsService.Update(id, request, User));
    }
}