using HelpDesk.Data;
using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Extensions;
using HelpDesk.Models;
using HelpDesk.Services.Auth;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Votes;

public class VotesService : IVotesService
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _user;

    public VotesService(AppDbContext db, ICurrentUser user)
    {
        _db = db;
        _user = user;
    }

    public async Task Vote(int issueId, VoteRequest request)
    {
        var issue = await _db.Issues.FindOrThrowAsync(issueId);

        var existingVote = await _db.Votes.FindAsync(issueId, _user.Id);

        if (existingVote is null)
            issue.Votes.Add(new Vote
            {
                Value = request.Vote,
                VoterId = _user.Id,
                IssueId = issueId
            });

        else
            existingVote.Value = request.Vote;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteUserVote(int issueId)
    {
        await _db.Votes
            .Where(v => v.IssueId == issueId && v.VoterId == _user.Id)
            .ExecuteDeleteAsync();
    }

    public async Task<VoteDto> GetUserVote(int issueId)
    {
        await _db.Issues.ExistsOrThrowAsync(issueId);

        var vote = await _db.Votes.FindAsync(issueId, _user.Id);

        return new VoteDto(vote?.Value);
    }
}
