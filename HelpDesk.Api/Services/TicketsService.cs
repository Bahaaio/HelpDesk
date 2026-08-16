using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Extensions;
using HelpDesk.Api.Mappers;
using HelpDesk.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class TicketsService : ITicketsService
{
    private readonly IAttachmentsService _attachmentsService;
    private readonly IAuthorizationGuard _authGuard;
    private readonly AppDbContext _db;
    private readonly ILogger<TicketsService> _logger;
    private readonly ICurrentUser _user;

    public TicketsService(AppDbContext db, ICurrentUser user, IAuthorizationGuard authGuard,
        ILogger<TicketsService> logger, IAttachmentsService attachmentsService)
    {
        _db = db;
        _user = user;
        _authGuard = authGuard;
        _logger = logger;
        _attachmentsService = attachmentsService;
    }

    public async Task<List<TicketDto>> GetAll([FromQuery] TicketQuery ticketQuery)
    {
        var query = _db.Tickets.AsNoTracking().ApplyFilters(ticketQuery);

        return await query
            .Select(TicketMapper.ToDtoExpression)
            .ToListAsync();
    }

    public async Task<List<TicketDto>> GetCurrentUserTickets(TicketQuery ticketQuery)
    {
        var query = _db.Tickets.AsNoTracking().ApplyFilters(ticketQuery)
            .Where(t => t.AuthorId == _user.Id);

        return await query
            .Select(TicketMapper.ToDtoExpression)
            .ToListAsync();
    }

    public async Task<TicketDto> GetById(int id)
    {
        var ticket = await _db.Tickets
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(TicketMapper.ToDtoExpression)
            .SingleOrDefaultAsync();

        return ticket ?? throw new NotFoundException($"Ticket with id {id} not found");
    }

    public async Task<TicketDto> Create(CreateTicketRequest request)
    {
        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            AuthorId = _user.Id
        };

        _db.Tickets.Add(ticket);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} created ticket {ticketId}", _user.Id, ticket.Id);

        await _db.Entry(ticket).Reference(t => t.Author).LoadAsync();
        return ticket.ToDto();
    }

    public async Task<TicketDto> Update(int id, UpdateTicketRequest request)
    {
        var ticket = await _db.Tickets
            .Where(t => t.Id == id)
            .Include(t => t.Author)
            .Include(t => t.AssignedTo)
            .Include(t => t.Tags)
            .Include(t => t.Attachments)
            .Include(t => t.Votes)
            .SingleOrDefaultAsync();

        if (ticket is null)
            throw new NotFoundException($"Ticket with id {id} not found");

        await _authGuard.AuthorizeOwnerOrTechnician(ticket);

        ticket.Title = request.Title;
        ticket.Description = request.Description;

        await _db.SaveChangesAsync();
        return ticket.ToDto();
    }

    public async Task UpdateStatus(int id, UpdateTicketStatusRequest request)
    {
        var ticket = await _db.Tickets.FindOrThrowAsync(id);

        await _authGuard.AuthorizeOwnerOrTechnician(ticket);

        if (ticket.Status == request.Status)
            return;

        var statusChange = new TicketStatusChange
        {
            TicketId = id,
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

    public async Task Delete(int id)
    {
        var ticket = await _db.Tickets.FindOrThrowAsync(id);

        await _authGuard.AuthorizeOwnerOrTechnician(ticket);

        await _attachmentsService.DeleteAttachmentsForTicket(id);

        _db.Remove(ticket);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} deleted ticket {ticketId}", _user.Id, id);
    }
}