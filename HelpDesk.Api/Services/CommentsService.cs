using System.Security.Claims;
using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class CommentsService(AppDbContext db)
{
    public async Task<List<CommentDto>> GetAll(int ticketId)
    {
        var ticket = await db.Tickets.SingleOrDefaultAsync(t => t.Id == ticketId);
        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        return await db.Comments
            .AsNoTracking()
            .Where(c => c.TicketId == ticketId)
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new CommentDto(c.Content, c.CreatedAt, c.Author.UserName!))
            .ToListAsync();
    }

    public async Task<CommentDto> Create(int ticketId, CreateCommentRequest request,
        ClaimsPrincipal user)
    {
        var ticket = await db.Tickets.SingleOrDefaultAsync(t => t.Id == ticketId);
        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var comment = new Comment
        {
            Content = request.Content,
            TicketId = ticketId,
            AuthorId = userId
        };

        await db.Comments.AddAsync(comment);
        await db.SaveChangesAsync();

        await db.Entry(comment).Reference(c => c.Author).LoadAsync();

        return new CommentDto(comment.Content, comment.CreatedAt, comment.Author.UserName!);
    }
}