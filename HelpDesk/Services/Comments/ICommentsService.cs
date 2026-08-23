using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;

namespace HelpDesk.Services.Comments;

/// <summary>
///     Manages comments on helpdesk issues.
/// </summary>
public interface ICommentsService
{
    /// <summary>
    ///     Returns all comments for a given issue, ordered by most recent.
    /// </summary>
    /// <param name="issueId">The ID of the issue to get comments for.</param>
    Task<List<CommentDto>> GetAll(int issueId);

    /// <summary>
    ///     Adds a comment to a issue as the current user.
    /// </summary>
    /// <param name="issueId">The ID of the issue to comment on.</param>
    /// <param name="request">The comment content.</param>
    Task<CommentDto> Create(int issueId, CreateCommentRequest request);

    /// <summary>
    ///     Updates a comment's content. Only the author or a technician may update.
    /// </summary>
    /// <param name="commentId">The ID of the comment to update.</param>
    /// <param name="request">The new comment content.</param>
    Task<CommentDto> Update(int commentId, UpdateCommentRequest request);

    /// <summary>
    ///     Deletes a comment. Only the author or a technician may delete.
    /// </summary>
    /// <param name="commentId">The ID of the comment to delete.</param>
    Task Delete(int commentId);
}
