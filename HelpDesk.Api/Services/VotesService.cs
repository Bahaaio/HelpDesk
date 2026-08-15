using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Extensions;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class VotesService : IVotesService
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _user;

    public VotesService(AppDbContext db, ICurrentUser user)
    {
        _db = db;
        _user = user;
    }

    public async Task Vote(int ticketId, VoteRequest request)
    {
        var ticket = await _db.Tickets.FindOrThrowAsync(ticketId);

        var existingVote = await _db.Votes.FindAsync(ticketId, _user.Id);

        if (existingVote is null)
            ticket.Votes.Add(new Vote
            {
                Value = request.Vote,
                VoterId = _user.Id,
                TicketId = ticketId
            });

        else
            existingVote.Value = request.Vote;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteUserVote(int ticketId)
    {
        await _db.Votes
            .Where(v => v.TicketId == ticketId && v.VoterId == _user.Id)
            .ExecuteDeleteAsync();
    }

    public async Task<VoteDto> GetUserVote(int ticketId)
    {
        await _db.Tickets.ExistsOrThrowAsync(ticketId);

        var vote = await _db.Votes.FindAsync(ticketId, _user.Id);

        return new VoteDto(vote?.Value);
    }
}