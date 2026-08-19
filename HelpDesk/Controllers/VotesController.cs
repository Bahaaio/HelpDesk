using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Models.Enums;
using HelpDesk.Services.Votes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

[Authorize(Roles = Role.Employee)]
[ApiController]
[Route("api/tickets/{ticketId:int}/[controller]")]
public class VotesController : ControllerBase
{
    private readonly IVotesService _votesService;

    public VotesController(IVotesService votesService)
    {
        _votesService = votesService;
    }

    [HttpGet("mine")]
    public async Task<ActionResult<VoteDto>> GetMyVote(int ticketId) =>
        Ok(await _votesService.GetUserVote(ticketId));

    [HttpDelete("mine")]
    public async Task<ActionResult> DeleteMyVote(int ticketId)
    {
        await _votesService.DeleteUserVote(ticketId);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> Vote(int ticketId, VoteRequest request)
    {
        await _votesService.Vote(ticketId, request);
        return NoContent();
    }
}
