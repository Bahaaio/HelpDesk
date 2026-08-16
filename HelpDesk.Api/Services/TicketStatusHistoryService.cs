using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class TicketStatusHistoryService : ITicketStatusHistoryService
{
    private readonly AppDbContext _db;

    public TicketStatusHistoryService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<StatusChangeDto>> GetStatusHistory(int ticketId)
    {
        await _db.Tickets.ExistsOrThrowAsync(ticketId);

        return await _db.TicketStatusChanges
            .AsNoTracking()
            .Where(s => s.TicketId == ticketId)
            .OrderBy(s => s.ChangedAt)
            .Select(s => new StatusChangeDto
            {
                FromStatus = s.FromStatus,
                ToStatus = s.ToStatus,
                ChangedByUsername = s.ChangedByUser.UserName!,
                ChangedAt = s.ChangedAt
            })
            .ToListAsync();
    }
}