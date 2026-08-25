using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Issues.Repositories;
using HelpDesk.Modules.Issues.Votes.Dtos;
using HelpDesk.Modules.Issues.Votes.Dtos.Requests;
using HelpDesk.Modules.Issues.Votes.Models;
using HelpDesk.Modules.Issues.Votes.Repositories;

namespace HelpDesk.Modules.Issues.Votes.Services.Implementations;

public class VotesService : IVotesService
{
    private readonly IIssuesRepository _issuesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _user;
    private readonly IVotesRepository _votesRepository;

    public VotesService(IVotesRepository votesRepository, IIssuesRepository issuesRepository,
        IUnitOfWork unitOfWork, ICurrentUser user)
    {
        _votesRepository = votesRepository;
        _issuesRepository = issuesRepository;
        _unitOfWork = unitOfWork;
        _user = user;
    }

    public async Task Vote(int issueId, VoteRequest request)
    {
        await _issuesRepository.ExistsOrThrowAsync(issueId);
        var existingVote = await _votesRepository.FindAsync(issueId, _user.Id);

        if (existingVote is null)
            _votesRepository.Add(new Vote
            {
                Value = request.Vote,
                VoterId = _user.Id,
                IssueId = issueId
            });
        else
            existingVote.Value = request.Vote;

        await _unitOfWork.SaveChangesAsync();
    }

    public Task DeleteUserVote(int issueId) =>
        _votesRepository.DeleteAsync(issueId, _user.Id);

    public async Task<VoteDto> GetUserVote(int issueId)
    {
        await _issuesRepository.ExistsOrThrowAsync(issueId);

        var vote = await _votesRepository.FindAsync(issueId, _user.Id);
        return new VoteDto(vote?.Value);
    }
}