using System.Security.Claims;
using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Models;
using HelpDesk.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class VotesService
{
    private readonly AppDbContext _db;

    public VotesService(AppDbContext db)
    {
        _db = db;
    }

    public async Task Vote(int ticketId, VoteRequest request, ClaimsPrincipal user)
    {
        var ticket = await _db.Tickets
            .Include(t => t.Votes)
            .SingleOrDefaultAsync(t => t.Id == ticketId);

        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var existingVote = await _db.Votes
            .SingleOrDefaultAsync(v => v.VoterId == userId && v.TicketId == ticketId);

        if (existingVote is null)
            ticket.Votes.Add(new Vote
            {
                Value = request.Vote,
                VoterId = userId,
                TicketId = ticketId
            });
        else
            existingVote.Value = request.Vote;

        await _db.SaveChangesAsync();
    }

    public async Task<VoteResponse> GetUserVote(int ticketId, ClaimsPrincipal user)
    {
        var ticket = await _db.Tickets
            .Include(t => t.Votes)
            .SingleOrDefaultAsync(t => t.Id == ticketId);

        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var vote = await _db.Votes
            .Where(v => v.TicketId == ticketId && v.VoterId == userId)
            .SingleOrDefaultAsync();

        var value = vote?.Value ?? VoteValue.None;
        return new VoteResponse(value);
    }
}