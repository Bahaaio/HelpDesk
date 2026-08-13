using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

public interface IVotesService
{
    Task Vote(int ticketId, VoteRequest request);
    Task<VoteResponse> GetUserVote(int ticketId);
}