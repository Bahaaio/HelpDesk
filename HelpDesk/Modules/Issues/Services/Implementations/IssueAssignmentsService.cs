using HelpDesk.Common.Exceptions;
using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Dtos.Requests;
using HelpDesk.Modules.Issues.Enums;
using HelpDesk.Modules.Issues.Repositories;

namespace HelpDesk.Modules.Issues.Services.Implementations;

public class IssueAssignmentsService : IIssueAssignmentsService
{
    private readonly IIssuesRepository _issuesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _user;

    public IssueAssignmentsService(IIssuesRepository issuesRepository, IUnitOfWork unitOfWork,
        ICurrentUser user)
    {
        _issuesRepository = issuesRepository;
        _unitOfWork = unitOfWork;
        _user = user;
    }

    public async Task AssignCurrentUser(int issueId)
    {
        var issue = await _issuesRepository.GetByIdAsync(issueId);

        if (issue.Status == Status.Closed)
            throw new ConflictException("Issue is already closed");

        if (issue.AssignedToId is not null)
            throw new ConflictException("Issue is already assigned to a user");

        issue.AssignedToId = _user.Id;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UnassignCurrentUser(int issueId)
    {
        var issue = await _issuesRepository.GetByIdAsync(issueId);

        if (issue.Status == Status.Closed)
            throw new ConflictException("Issue is already closed");

        if (issue.AssignedToId != _user.Id)
            throw new ForbiddenException("Issue is not assigned to the current user");

        issue.AssignedToId = null;
        await _unitOfWork.SaveChangesAsync();
    }

    public Task<List<IssueDto>> GetCurrentUserAssignedIssues(IssueQuery issueQuery) =>
        _issuesRepository.GetAllAssignedToUserAsync(_user.Id, issueQuery);
}