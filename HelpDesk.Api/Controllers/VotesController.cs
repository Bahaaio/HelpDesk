using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Models.Enums;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[Authorize(Roles = Role.Employee)]
[ApiController]
[Route("api/tickets/{ticketId:int}/[controller]")]
public class VotesController(VotesService votesService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Vote(int ticketId, VoteRequest request)
    {
        await votesService.Vote(ticketId, request, User);
        return NoContent();
    }
}