using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services.Comments;

/// <summary>
///     Manages comments on helpdesk tickets.
/// </summary>
public interface ICommentsService
{
    /// <summary>
    ///     Returns all comments for a given ticket, ordered by most recent.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to get comments for.</param>
    Task<List<CommentDto>> GetAll(int ticketId);

    /// <summary>
    ///     Adds a comment to a ticket as the current user.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to comment on.</param>
    /// <param name="request">The comment content.</param>
    Task<CommentDto> Create(int ticketId, CreateCommentRequest request);

    /// <summary>
    ///     Deletes a comment. Only the author or a technician may delete.
    /// </summary>
    /// <param name="commentId">The ID of the comment to delete.</param>
    Task Delete(int commentId);
}