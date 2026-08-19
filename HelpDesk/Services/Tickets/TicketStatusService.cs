using HelpDesk.Data;
using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Extensions;
using HelpDesk.Models;
using HelpDesk.Services.Auth;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services.Tickets;

public class TicketStatusService : ITicketStatusService
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly AppDbContext _db;
    private readonly ILogger<TicketStatusService> _logger;
    private readonly ICurrentUser _user;

    public TicketStatusService(AppDbContext db, ICurrentUser user, IAuthorizationGuard authGuard,
        ILogger<TicketStatusService> logger)
    {
        _db = db;
        _user = user;
        _authGuard = authGuard;
        _logger = logger;
    }

    public async Task UpdateStatus(int ticketId, UpdateTicketStatusRequest request)
    {
        var ticket = await _db.Tickets.FindOrThrowAsync(ticketId);

        await _authGuard.AuthorizeOwnerOrTechnician(ticket);

        if (ticket.Status == request.Status)
            return;

        var statusChange = new TicketStatusChange
        {
            TicketId = ticketId,
            FromStatus = ticket.Status,
            ToStatus = request.Status,
            ChangedByUserId = _user.Id
        };

        ticket.Status = request.Status;
        _db.TicketStatusChanges.Add(statusChange);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} updated ticket {ticketId} status from {from} to {to}",
            _user.Id, ticket.Id, statusChange.FromStatus, statusChange.ToStatus);
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
