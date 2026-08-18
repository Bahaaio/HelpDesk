using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Extensions;
using HelpDesk.Api.Mappers;
using HelpDesk.Api.Models;
using HelpDesk.Api.Services.Auth;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services.Comments;

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
        await _db.Tickets.ExistsOrThrowAsync(ticketId);

        return await _db.Comments
            .AsNoTracking()
            .Where(c => c.TicketId == ticketId)
            .OrderByDescending(c => c.CreatedAt)
            .Select(CommentMapper.ToDtoExpression)
            .ToListAsync();
    }

    public async Task<CommentDto> Create(int ticketId, CreateCommentRequest request)
    {
        await _db.Tickets.ExistsOrThrowAsync(ticketId);

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
        var comment = await _db.Comments.FindOrThrowAsync(commentId);

        await _authGuard.AuthorizeOwnerOrTechnician(comment);

        _db.Remove(comment);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} deleted comment {commentId}",
            _user.Id, commentId);
    }
}