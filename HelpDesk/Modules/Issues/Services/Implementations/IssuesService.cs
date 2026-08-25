using HelpDesk.Common.Authorization;
using HelpDesk.Common.Exceptions;
using HelpDesk.Common.Extensions;
using HelpDesk.Data;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Dtos.Requests;
using HelpDesk.Modules.Issues.Extensions;
using HelpDesk.Modules.Issues.Mappers;
using HelpDesk.Modules.Issues.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Issues.Services.Implementations;

public class IssuesService : IIssuesService
{
    private readonly IAttachmentsService<Issue> _attachmentsService;
    private readonly IAuthorizationGuard _authGuard;
    private readonly AppDbContext _db;
    private readonly ILogger<IssuesService> _logger;
    private readonly ICurrentUser _user;

    public IssuesService(AppDbContext db, ICurrentUser user, IAuthorizationGuard authGuard,
        ILogger<IssuesService> logger, IAttachmentsService<Issue> attachmentsService)
    {
        _db = db;
        _user = user;
        _authGuard = authGuard;
        _logger = logger;
        _attachmentsService = attachmentsService;
    }

    public async Task<List<IssueDto>> GetAll([FromQuery] IssueQuery issueQuery)
    {
        var query = _db.Issues.AsNoTracking().ApplyFilters(issueQuery);

        return await query
            .Select(IssueMapper.ToDtoExpression)
            .ToListAsync();
    }

    public async Task<List<IssueDto>> GetCurrentUserIssues(IssueQuery issueQuery)
    {
        var query = _db.Issues.AsNoTracking().ApplyFilters(issueQuery)
            .Where(t => t.AuthorId == _user.Id);

        return await query
            .Select(IssueMapper.ToDtoExpression)
            .ToListAsync();
    }

    public async Task<IssueDto> GetById(int id)
    {
        var issue = await _db.Issues
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(IssueMapper.ToDtoExpression)
            .SingleOrDefaultAsync();

        return issue ?? throw new NotFoundException($"Issue with id {id} not found");
    }

    public async Task<IssueDto> Create(CreateIssueRequest request)
    {
        var issue = new Issue
        {
            Title = request.Title,
            Description = request.Description,
            AuthorId = _user.Id
        };

        _db.Issues.Add(issue);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} created issue {issueId}", _user.Id, issue.Id);

        await _db.Entry(issue).Reference(t => t.Author).LoadAsync();
        return issue.ToDto();
    }

    public async Task<IssueDto> Update(int id, UpdateIssueRequest request)
    {
        var issue = await _db.Issues
            .Where(t => t.Id == id)
            .Include(t => t.Author)
            .Include(t => t.AssignedTo)
            .Include(t => t.Tags)
            .Include(t => t.Votes)
            .AsSplitQuery()
            .SingleOrDefaultAsync();

        if (issue is null)
            throw new NotFoundException($"Issue with id {id} not found");

        await _authGuard.AuthorizeOwnerOrTechnician(issue);

        issue.Title = request.Title;
        issue.Description = request.Description;

        await _db.SaveChangesAsync();
        return issue.ToDto();
    }

    public async Task Delete(int id)
    {
        var issue = await _db.Issues.FindOrThrowAsync(id);

        await _authGuard.AuthorizeOwnerOrTechnician(issue);

        await _attachmentsService.DeleteAll(id);

        _db.Remove(issue);
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {userId} deleted issue {issueId}", _user.Id, id);
    }
}