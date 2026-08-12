using HelpDesk.Api.Models.Enums;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[Authorize(Roles = Role.Technician)]
[ApiController]
[Route("api/tickets/{ticketId:int}/tags")]
public class TicketTagsController(TicketTagsService ticketTagsService) : ControllerBase
{
    [HttpPut]
    public async Task<ActionResult<List<string>>> UpdateTags(int ticketId, List<string> tags)
    {
        return Ok(await ticketTagsService.UpdateTags(ticketId, tags));
    }
}