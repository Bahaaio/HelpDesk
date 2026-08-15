using HelpDesk.Api.Authorization.Requirements;
using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Mappers;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class CommentsService : ICommentsService
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly AppDbContext _db;
    private readonly ILogger<CommentsService> _logger;
    private readonly ICurrentUser _user;

    public CommentsService(AppDbContext db, ICurrentUser user, ILogger<CommentsService> logger,
        IAuthorizationGuard authGuard)
    {
        _authGuard = authGuard;
        _db = db;
        _user = user;
        _logger = logger;
    }

    public async Task<List<CommentDto>> GetAll(int ticketId)
    {
        var ticket = await _db.Tickets.FindAsync(ticketId);
        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        return await _db.Comments
            .AsNoTracking()
            .Where(c => c.TicketId == ticketId)
            .OrderByDescending(c => c.CreatedAt)
            .Select(CommentMapper.ToDtoExpression)
            .ToListAsync();
    }

    public async Task<CommentDto> Create(int ticketId, CreateCommentRequest request)
    {
        var ticket = await _db.Tickets.FindAsync(ticketId);
        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        var comment = new Comment
        {
            Content = request.Content,
            TicketId = ticketId,
            AuthorId = _user.Id
        };

        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} created comment {commentId} on ticket {ticketId}",
            _user.Id, comment.Id, ticketId);

        await _db.Entry(comment).Reference(c => c.Author).LoadAsync();

        return comment.ToDto();
    }

    public async Task Delete(int commentId)
    {
        var comment = await _db.Comments.FindAsync(commentId);
        if (comment is null)
            throw new NotFoundException($"Comment with id {commentId} not found");

        await _authGuard.Authorize(comment, new CommentAuthorOrTechnicianRequirement());

        _db.Remove(comment);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} deleted comment {commentId}",
            _user.Id, commentId);
    }
}