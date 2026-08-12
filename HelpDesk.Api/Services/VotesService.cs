using System.Security.Claims;
using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class VotesService(AppDbContext db)
{
    public async Task Vote(int ticketId, VoteRequest request, ClaimsPrincipal user)
    {
        var ticket = await db.Tickets
            .Include(t => t.Votes)
            .SingleOrDefaultAsync(t => t.Id == ticketId);

        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var existingVote = await db.Votes
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

        await db.SaveChangesAsync();
    }
}