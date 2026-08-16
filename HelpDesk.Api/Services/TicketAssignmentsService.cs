using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Extensions;
using HelpDesk.Api.Mappers;
using HelpDesk.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class TicketAssignmentsService : ITicketAssignmentsService
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _user;

    public TicketAssignmentsService(ICurrentUser user, AppDbContext db)
    {
        _user = user;
        _db = db;
    }

    public async Task AssignCurrentUser(int ticketId)
    {
        var ticket = await _db.Tickets.FindOrThrowAsync(ticketId);

        if (ticket.Status == Status.Closed)
            throw new ConflictException("Ticket is already closed");

        if (ticket.AssignedToId is not null)
            throw new ConflictException("Ticket is already assigned to a user");

        ticket.AssignedToId = _user.Id;
        await _db.SaveChangesAsync();
    }

    public async Task UnassignCurrentUser(int ticketId)
    {
        var ticket = await _db.Tickets.FindOrThrowAsync(ticketId);

        if (ticket.Status == Status.Closed)
            throw new ConflictException("Ticket is already closed");

        if (ticket.AssignedToId != _user.Id)
            throw new ForbiddenException("Ticket is not assigned to the current user");

        ticket.AssignedToId = null;
        await _db.SaveChangesAsync();
    }

    public async Task<List<TicketDto>> GetCurrentUserAssignedTickets(TicketQuery ticketQuery)
    {
        return await _db.Tickets
            .AsNoTracking()
            .ApplyFilters(ticketQuery)
            .Where(t => t.AssignedToId == _user.Id)
            .Select(TicketMapper.ToDtoExpression)
            .ToListAsync();
    }
}