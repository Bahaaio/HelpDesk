using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Authorization.Extensions;
using HelpDesk.Modules.Authorization.Services;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Dtos.Requests;
using HelpDesk.Modules.Issues.Models;
using HelpDesk.Modules.Issues.Repositories;

namespace HelpDesk.Modules.Issues.Services.Implementations;

public class IssueStatusService : IIssueStatusService
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly IIssuesRepository _issuesRepository;
    private readonly ILogger<IssueStatusService> _logger;
    private readonly IIssueStatusChangeRepository _statusChangeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _user;

    public IssueStatusService(IIssuesRepository issuesRepository,
        IIssueStatusChangeRepository statusChangeRepository, IUnitOfWork unitOfWork,
        ICurrentUser user, IAuthorizationGuard authGuard, ILogger<IssueStatusService> logger)
    {
        _issuesRepository = issuesRepository;
        _statusChangeRepository = statusChangeRepository;
        _unitOfWork = unitOfWork;
        _user = user;
        _authGuard = authGuard;
        _logger = logger;
    }

    public async Task UpdateStatus(int issueId, UpdateIssueStatusRequest request)
    {
        var issue = await _issuesRepository.GetByIdAsync(issueId);

        await _authGuard.AuthorizeOwnerOrTechnician(issue);

        if (issue.Status == request.Status)
            return;

        var statusChange = new IssueStatusChange
        {
            IssueId = issueId,
            FromStatus = issue.Status,
            ToStatus = request.Status,
            ChangedByUserId = _user.Id
        };

        issue.Status = request.Status;
        _statusChangeRepository.Add(statusChange);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("User {userId} updated issue {issueId} status from {from} to {to}",
            _user.Id, issue.Id, statusChange.FromStatus, statusChange.ToStatus);
    }

    public async Task<List<StatusChangeDto>> GetStatusHistory(int issueId)
    {
        await _issuesRepository.ExistsOrThrowAsync(issueId);
        return await _statusChangeRepository.GetHistoryAsync(issueId);
    }
}