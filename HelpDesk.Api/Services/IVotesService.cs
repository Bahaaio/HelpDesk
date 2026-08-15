using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

/// <summary>
///     Manages up/down votes on tickets. Employees may vote; each user may have one vote per ticket.
/// </summary>
public interface IVotesService
{
    /// <summary>
    ///     Casts or updates the current user's vote on a ticket.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to vote on.</param>
    /// <param name="request">The vote value (Upvote or Downvote).</param>
    Task Vote(int ticketId, VoteRequest request);

    /// <summary>
    ///     Deletes the current user's vote on a ticket.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to delete the vote for.</param>
    Task DeleteUserVote(int ticketId);

    /// <summary>
    ///     Returns the current user's vote on a ticket, if any.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to check the vote for.</param>
    Task<VoteDto> GetUserVote(int ticketId);
}