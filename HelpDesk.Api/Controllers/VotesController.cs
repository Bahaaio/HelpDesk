using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Models.Enums;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

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
    public async Task<ActionResult<VoteResponse>> GetMyVote(int ticketId)
    {
        return Ok(await _votesService.GetUserVote(ticketId));
    }

    [HttpPost]
    public async Task<ActionResult> Vote(int ticketId, VoteRequest request)
    {
        await _votesService.Vote(ticketId, request);
        return NoContent();
    }
}