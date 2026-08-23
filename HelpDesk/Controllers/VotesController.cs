using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Models.Enums;
using HelpDesk.Services.Votes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

[Authorize(Roles = Role.Employee)]
[ApiController]
[Route("api/issues/{issueId:int}/[controller]")]
public class VotesController : ControllerBase
{
    private readonly IVotesService _votesService;

    public VotesController(IVotesService votesService)
    {
        _votesService = votesService;
    }

    [HttpGet("mine")]
    public async Task<ActionResult<VoteDto>> GetMyVote(int issueId) =>
        Ok(await _votesService.GetUserVote(issueId));

    [HttpDelete("mine")]
    public async Task<ActionResult> DeleteMyVote(int issueId)
    {
        await _votesService.DeleteUserVote(issueId);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> Vote(int issueId, VoteRequest request)
    {
        await _votesService.Vote(issueId, request);
        return NoContent();
    }
}
