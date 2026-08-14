using HelpDesk.Api.Authorization.Requirements;
using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Mappers;
using HelpDesk.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class TicketsService : ITicketsService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly AppDbContext _db;
    private readonly ICurrentUser _user;

    public TicketsService(AppDbContext db, IAuthorizationService authorizationService,
        ICurrentUser user)
    {
        _db = db;
        _authorizationService = authorizationService;
        _user = user;
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

    public async Task<TicketDto?> GetById(int id)
    {
        return await _db.Tickets
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(TicketMapper.ToDtoExpression)
            .FirstOrDefaultAsync();
    }

    public async Task<TicketDto> Create(CreateTicketRequest request)
    {
        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            AuthorId = _user.Id
        };

        await _db.Tickets.AddAsync(ticket);
        await _db.SaveChangesAsync();

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
            .FirstOrDefaultAsync();

        if (ticket is null)
            throw new NotFoundException($"Ticket with id {id} not found");

        var result = await _authorizationService.AuthorizeAsync(
            _user.Principal,
            ticket,
            new TicketOwnerOrTechnicianRequirement()
        );

        if (!result.Succeeded)
            throw new ForbiddenException("You are not authorized to update this ticket");

        ticket.Title = request.Title;
        ticket.Description = request.Description;

        await _db.SaveChangesAsync();
        return ticket.ToDto();
    }

    public async Task UpdateStatus(int id, TicketStatusUpdateRequest request)
    {
        var ticket = await _db.Tickets.FindAsync(id);
        if (ticket is null)
            throw new NotFoundException($"Ticket with id {id} not found");

        var result = await _authorizationService.AuthorizeAsync(
            _user.Principal,
            ticket,
            new TicketOwnerOrTechnicianRequirement()
        );

        if (!result.Succeeded)
            throw new ForbiddenException("You are not authorized to update this ticket");

        ticket.Status = request.Status;
        await _db.SaveChangesAsync();
    }

    private IQueryable<Ticket> GetTicketQuery(TicketQuery ticketQuery)
    {
        var query = _db.Tickets.AsNoTracking();

        if (ticketQuery.Status is not null)
            query = query.Where(t => t.Status == ticketQuery.Status);

        if (ticketQuery.Author is not null)
            query = query.Where(t => t.Author.UserName!.ToLower() == ticketQuery.Author.ToLower());

        if (ticketQuery.Tag is not null)
            query = query.Where(t =>
                t.Tags.Any(tag =>
                    tag.Name == ticketQuery.Tag.ToLower())
            );

        if (ticketQuery.Q is not null)
            query = query.Where(t =>
                t.Title.ToLower().Contains(ticketQuery.Q.ToLower()) ||
                (t.Description != null &&
                 t.Description.ToLower().Contains(ticketQuery.Q.ToLower()))
            );

        return query;
    }
}