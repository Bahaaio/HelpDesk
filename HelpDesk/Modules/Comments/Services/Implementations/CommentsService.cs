using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Authorization.Extensions;
using HelpDesk.Modules.Authorization.Services;
using HelpDesk.Modules.Comments.Dtos;
using HelpDesk.Modules.Comments.Dtos.Requests;
using HelpDesk.Modules.Comments.Mappers;
using HelpDesk.Modules.Comments.Models;
using HelpDesk.Modules.Comments.Repositories;
using HelpDesk.Modules.Issues.Repositories;

namespace HelpDesk.Modules.Comments.Services.Implementations;

public class CommentsService : ICommentsService
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly ICommentsRepository _commentsRepository;
    private readonly IIssuesRepository _issuesRepository;
    private readonly ILogger<CommentsService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _user;

    public CommentsService(ICurrentUser user, ILogger<CommentsService> logger,
        IAuthorizationGuard authGuard, ICommentsRepository commentsRepository,
        IUnitOfWork unitOfWork, IIssuesRepository issuesRepository)
    {
        _authGuard = authGuard;
        _commentsRepository = commentsRepository;
        _unitOfWork = unitOfWork;
        _issuesRepository = issuesRepository;
        _user = user;
        _logger = logger;
    }

    public Task<List<CommentDto>> GetAll(int issueId) =>
        _commentsRepository.GetAllByIssueIdAsync(issueId);

    public async Task<CommentDto> Create(int issueId, CreateCommentRequest request)
    {
        await _issuesRepository.ExistsOrThrowAsync(issueId);

        var comment = new Comment
        {
            Content = request.Content,
            IssueId = issueId,
            AuthorId = _user.Id
        };

        _commentsRepository.Add(comment);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("User {userId} created comment {commentId} on issue {issueId}",
            _user.Id, comment.Id, issueId);

        // Author is always the current user, build the DTO directly without an extra DB round trip.
        return new CommentDto(comment.Id, comment.Content, comment.CreatedAt, _user.UserName, []);
    }

    public async Task<CommentDto> Update(int commentId, UpdateCommentRequest request)
    {
        var comment = await _commentsRepository.GetByIdAsync(commentId);
        await _authGuard.AuthorizeOwnerOrTechnician(comment);

        comment.Content = request.Content;
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("User {userId} updated comment {commentId}", _user.Id, commentId);

        return comment.ToDto();
    }

    public async Task Delete(int commentId)
    {
        var comment = await _commentsRepository.GetByIdAsync(commentId);

        await _authGuard.AuthorizeOwnerOrTechnician(comment);

        _commentsRepository.Remove(comment);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("User {userId} deleted comment {commentId}", _user.Id, commentId);
    }
}