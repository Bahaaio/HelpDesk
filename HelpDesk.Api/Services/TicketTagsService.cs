using HelpDesk.Api.Data;
using HelpDesk.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class TicketTagsService : ITicketTagsService
{
    private readonly AppDbContext _db;

    public TicketTagsService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<string>> UpdateTags(int ticketId, List<string> tags)
    {
        var ticket = await _db.Tickets
            .Include(t => t.Tags)
            .SingleOrDefaultAsync(t => t.Id == ticketId);

        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        var lowerTags = tags.Select(n => n.ToLower());

        var tagEntities = await _db.Tags
            .Where(t => lowerTags.Contains(t.Name))
            .ToListAsync();

        var missing = lowerTags.Except(tagEntities.Select(t => t.Name)).ToList();
        if (missing.Count != 0)
            throw new NotFoundException($"Tags {string.Join(", ", missing)} not found");

        ticket.Tags = tagEntities;
        await _db.SaveChangesAsync();

        return ticket.Tags.Select(t => t.Name).ToList();
    }
}