using HelpDesk.Common.Authorization;
using HelpDesk.Common.Extensions;
using HelpDesk.Data;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Dtos.Requests;
using HelpDesk.Modules.Issues.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Issues.Services.Implementations;

public class IssueStatusService : IIssueStatusService
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly AppDbContext _db;
    private readonly ILogger<IssueStatusService> _logger;
    private readonly ICurrentUser _user;

    public IssueStatusService(AppDbContext db, ICurrentUser user, IAuthorizationGuard authGuard,
        ILogger<IssueStatusService> logger)
    {
        _db = db;
        _user = user;
        _authGuard = authGuard;
        _logger = logger;
    }

    public async Task UpdateStatus(int issueId, UpdateIssueStatusRequest request)
    {
        var issue = await _db.Issues.FindOrThrowAsync(issueId);

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
        _db.IssueStatusChanges.Add(statusChange);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} updated issue {issueId} status from {from} to {to}",
            _user.Id, issue.Id, statusChange.FromStatus, statusChange.ToStatus);
    }

    public async Task<List<StatusChangeDto>> GetStatusHistory(int issueId)
    {
        await _db.Issues.ExistsOrThrowAsync(issueId);

        return await _db.IssueStatusChanges
            .AsNoTracking()
            .Where(s => s.IssueId == issueId)
            .OrderBy(s => s.ChangedAt)
            .Select(s => new StatusChangeDto
            {
                FromStatus = s.FromStatus,
                ToStatus = s.ToStatus,
                ChangedByUsername = s.ChangedByUser.UserName!,
                ChangedAt = s.ChangedAt
            })
            .ToListAsync();
    }
}