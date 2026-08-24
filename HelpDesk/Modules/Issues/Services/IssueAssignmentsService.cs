using HelpDesk.Common.Exceptions;
using HelpDesk.Common.Extensions;
using HelpDesk.Data;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Dtos.Requests;
using HelpDesk.Modules.Issues.Enums;
using HelpDesk.Modules.Issues.Extensions;
using HelpDesk.Modules.Issues.Mappers;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Issues.Services;

public class IssueAssignmentsService : IIssueAssignmentsService
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _user;

    public IssueAssignmentsService(ICurrentUser user, AppDbContext db)
    {
        _user = user;
        _db = db;
    }

    public async Task AssignCurrentUser(int issueId)
    {
        var issue = await _db.Issues.FindOrThrowAsync(issueId);

        if (issue.Status == Status.Closed)
            throw new ConflictException("Issue is already closed");

        if (issue.AssignedToId is not null)
            throw new ConflictException("Issue is already assigned to a user");

        issue.AssignedToId = _user.Id;
        await _db.SaveChangesAsync();
    }

    public async Task UnassignCurrentUser(int issueId)
    {
        var issue = await _db.Issues.FindOrThrowAsync(issueId);

        if (issue.Status == Status.Closed)
            throw new ConflictException("Issue is already closed");

        if (issue.AssignedToId != _user.Id)
            throw new ForbiddenException("Issue is not assigned to the current user");

        issue.AssignedToId = null;
        await _db.SaveChangesAsync();
    }

    public async Task<List<IssueDto>> GetCurrentUserAssignedIssues(IssueQuery issueQuery)
    {
        return await _db.Issues
            .AsNoTracking()
            .ApplyFilters(issueQuery)
            .Where(t => t.AssignedToId == _user.Id)
            .Select(IssueMapper.ToDtoExpression)
            .ToListAsync();
    }
}