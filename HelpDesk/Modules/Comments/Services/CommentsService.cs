using HelpDesk.Common.Authorization;
using HelpDesk.Common.Extensions;
using HelpDesk.Data;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Comments.Dtos;
using HelpDesk.Modules.Comments.Dtos.Requests;
using HelpDesk.Modules.Comments.Mappers;
using HelpDesk.Modules.Comments.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Comments.Services;

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

    public async Task<List<CommentDto>> GetAll(int issueId)
    {
        await _db.Issues.ExistsOrThrowAsync(issueId);

        return await _db.Comments
            .AsNoTracking()
            .Where(c => c.IssueId == issueId)
            .OrderByDescending(c => c.CreatedAt)
            .Select(CommentMapper.ToDtoExpression)
            .ToListAsync();
    }

    public async Task<CommentDto> Create(int issueId, CreateCommentRequest request)
    {
        await _db.Issues.ExistsOrThrowAsync(issueId);

        var comment = new Comment
        {
            Content = request.Content,
            IssueId = issueId,
            AuthorId = _user.Id
        };

        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} created comment {commentId} on issue {issueId}",
            _user.Id, comment.Id, issueId);

        await _db.Entry(comment).Reference(c => c.Author).LoadAsync();

        return comment.ToDto();
    }

    public async Task<CommentDto> Update(int commentId, UpdateCommentRequest request)
    {
        var comment = await _db.Comments.FindOrThrowAsync(commentId);

        await _authGuard.AuthorizeOwnerOrTechnician(comment);

        comment.Content = request.Content;
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} updated comment {commentId}",
            _user.Id, commentId);

        return await _db.Comments
            .AsNoTracking()
            .Where(c => c.Id == commentId)
            .Select(CommentMapper.ToDtoExpression)
            .SingleAsync();
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