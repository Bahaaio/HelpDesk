using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Models;
using HelpDesk.Api.Models.Enums;
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
        var ticket = await _db.Tickets
            .Include(t => t.Votes)
            .SingleOrDefaultAsync(t => t.Id == ticketId);

        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        var existingVote = await _db.Votes
            .SingleOrDefaultAsync(v => v.VoterId == _user.Id && v.TicketId == ticketId);

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

    public async Task<VoteResponse> GetUserVote(int ticketId)
    {
        var ticket = await _db.Tickets.FindAsync(ticketId);

        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        var vote = await _db.Votes
            .Where(v => v.TicketId == ticketId && v.VoterId == _user.Id)
            .SingleOrDefaultAsync();

        var value = vote?.Value ?? VoteValue.None;
        return new VoteResponse(value);
    }
}