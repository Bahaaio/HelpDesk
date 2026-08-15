using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Extensions;
using HelpDesk.Api.Mappers;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class TicketsService : ITicketsService
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly AppDbContext _db;
    private readonly ILogger<TicketsService> _logger;
    private readonly ICurrentUser _user;

    public TicketsService(AppDbContext db, ICurrentUser user, IAuthorizationGuard authGuard,
        ILogger<TicketsService> logger)
    {
        _db = db;
        _user = user;
        _authGuard = authGuard;
        _logger = logger;
    }

    public async Task<List<TicketDto>> GetAll(TicketQuery ticketQuery)
    {
        var query = GetTicketQuery(ticketQuery);

        return await query
            .Select(TicketMapper.ToDtoExpression)
            .ToListAsync();
    }

    public async Task<List<TicketDto>> GetCurrentUserTickets(TicketQuery ticketQuery)
    {
        var query = GetTicketQuery(ticketQuery);

        query = query.Where(t => t.AuthorId == _user.Id);

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
        var ticket = await _db.Tickets.FindAsync(id);
        if (ticket is null)
            throw new NotFoundException($"Ticket with id {id} not found");

        await _authGuard.AuthorizeOwnerOrTechnician(ticket);

        if (ticket.Status == request.Status)
            return;

        ticket.Status = request.Status;
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} updated ticket {ticketId} status to {status}",
            _user.Id, ticket.Id, ticket.Status);
    }

    private IQueryable<Ticket> GetTicketQuery(TicketQuery ticketQuery)
    {
        var query = _db.Tickets.AsNoTracking();

        if (ticketQuery.Status is not null)
            query = query.Where(t => t.Status == ticketQuery.Status);

        if (ticketQuery.Author is not null)
            query = query.Where(t =>
                EF.Functions.ILike(t.Author.UserName!, ticketQuery.Author)
            );

        if (ticketQuery.Tag is not null)
            query = query.Where(t =>
                t.Tags.Any(tag =>
                    EF.Functions.ILike(tag.Name, ticketQuery.Tag))
            );

        if (ticketQuery.Query is not null)
        {
            var pattern = $"%{ticketQuery.Query}%";
            query = query.Where(t =>
                EF.Functions.ILike(t.Title, pattern) ||
                (t.Description != null && EF.Functions.ILike(t.Description, pattern))
            );
        }

        return query;
    }
}