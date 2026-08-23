using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;

namespace HelpDesk.Services.Votes;

/// <summary>
///     Manages up/down votes on issues. Employees may vote; each user may have one vote per issue.
/// </summary>
public interface IVotesService
{
    /// <summary>
    ///     Casts or updates the current user's vote on a issue.
    /// </summary>
    /// <param name="issueId">The ID of the issue to vote on.</param>
    /// <param name="request">The vote value (Upvote or Downvote).</param>
    Task Vote(int issueId, VoteRequest request);

    /// <summary>
    ///     Deletes the current user's vote on a issue.
    /// </summary>
    /// <param name="issueId">The ID of the issue to delete the vote for.</param>
    Task DeleteUserVote(int issueId);

    /// <summary>
    ///     Returns the current user's vote on a issue, if any.
    /// </summary>
    /// <param name="issueId">The ID of the issue to check the vote for.</param>
    Task<VoteDto> GetUserVote(int issueId);
}
