using System.Security.Claims;
using HelpDesk.Api.Authorization.Requirements;
using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class TicketsService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly AppDbContext _db;

    public TicketsService(AppDbContext db, IAuthorizationService authorizationService)
    {
        _db = db;
        _authorizationService = authorizationService;
    }

    public async Task<List<TicketDto>> GetAll(TicketQuery ticketQuery)
    {
        var query = GetTicketQuery(ticketQuery);

        return await query
            .Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                AuthorUsername = t.Author.UserName!,
                Tags = t.Tags.Select(tag => tag.Name).ToList(),
                Attachments = t.Attachments.Select(a => a.Id).ToList(),
                VoteScore = t.Votes.Sum(v => (int)v.Value)
            })
            .ToListAsync();
    }

    public async Task<List<TicketDto>> GetCurrentUserTickets(TicketQuery ticketQuery,
        ClaimsPrincipal user)
    {
        var query = GetTicketQuery(ticketQuery);
        var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

        query = query.Where(t => t.AuthorId == userId);

        return await query
            .Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                AuthorUsername = t.Author.UserName!,
                Tags = t.Tags.Select(tag => tag.Name).ToList(),
                Attachments = t.Attachments.Select(a => a.Id).ToList(),
                VoteScore = t.Votes.Sum(v => (int)v.Value)
            })
            .ToListAsync();
    }

    public async Task<TicketDto?> GetById(int id)
    {
        return await _db.Tickets
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                AuthorUsername = t.Author.UserName!,
                Tags = t.Tags.Select(tag => tag.Name).ToList(),
                Attachments = t.Attachments.Select(a => a.Id).ToList(),
                VoteScore = t.Votes.Sum(v => (int)v.Value)
            })
            .FirstOrDefaultAsync();
    }

    public async Task<TicketDto> Create(CreateTicketRequest request, ClaimsPrincipal user)
    {
        var claim = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(claim, out var userId))
            throw new UnauthorizedException($"Invalid user id: {claim}");

        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            AuthorId = userId
        };

        await _db.Tickets.AddAsync(ticket);
        await _db.SaveChangesAsync();

        var userName = user.FindFirstValue(ClaimTypes.Name)!;

        return new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            CreatedAt = ticket.CreatedAt,
            AuthorUsername = userName,
            Tags = ticket.Tags.Select(tag => tag.Name).ToList(),
            Attachments = ticket.Attachments.Select(a => a.Id).ToList(),
            VoteScore = 0
        };
    }

    public async Task<TicketDto> Update(int id, UpdateTicketRequest request, ClaimsPrincipal user)
    {
        var ticket = await _db.Tickets
            .Where(t => t.Id == id)
            .Include(t => t.Tags)
            .Include(t => t.Attachments)
            .Include(t => t.Votes)
            .FirstOrDefaultAsync();

        if (ticket is null)
            throw new NotFoundException($"Ticket with id {id} not found");

        var result = await _authorizationService.AuthorizeAsync(
            user,
            ticket,
            new TicketOwnerOrTechnicianRequirement()
        );

        if (!result.Succeeded)
            throw new ForbiddenException("You are not authorized to update this ticket");

        ticket.Title = request.Title;
        ticket.Description = request.Description;

        await _db.SaveChangesAsync();

        var userName = user.FindFirstValue(ClaimTypes.Name)!;

        return new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            CreatedAt = ticket.CreatedAt,
            AuthorUsername = userName,
            Tags = ticket.Tags.Select(tag => tag.Name).ToList(),
            Attachments = ticket.Attachments.Select(a => a.Id).ToList(),
            VoteScore = ticket.Votes.Sum(v => (int)v.Value)
        };
    }

    public async Task UpdateStatus(int id, TicketStatusUpdateRequest request, ClaimsPrincipal user)
    {
        var ticket = await _db.Tickets.FindAsync(id);
        if (ticket is null)
            throw new NotFoundException($"Ticket with id {id} not found");

        var result = await _authorizationService.AuthorizeAsync(
            user,
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