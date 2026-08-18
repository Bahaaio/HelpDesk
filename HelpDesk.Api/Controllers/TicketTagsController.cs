using HelpDesk.Api.Models.Enums;
using HelpDesk.Api.Services.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[Authorize(Roles = Role.Technician)]
[ApiController]
[Route("api/tickets/{ticketId:int}/tags")]
public class TicketTagsController : ControllerBase
{
    private readonly ITicketTagsService _ticketTagsService;

    public TicketTagsController(ITicketTagsService ticketTagsService)
    {
        _ticketTagsService = ticketTagsService;
    }

    [HttpPut]
    public async Task<ActionResult<List<string>>> UpdateTags(int ticketId, List<string> tags) =>
        Ok(await _ticketTagsService.UpdateTags(ticketId, tags));
}