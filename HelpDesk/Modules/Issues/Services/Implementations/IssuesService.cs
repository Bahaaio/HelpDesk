using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Authorization.Extensions;
using HelpDesk.Modules.Authorization.Services;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Dtos.Requests;
using HelpDesk.Modules.Issues.Mappers;
using HelpDesk.Modules.Issues.Models;
using HelpDesk.Modules.Issues.Repositories;

namespace HelpDesk.Modules.Issues.Services.Implementations;

public class IssuesService : IIssuesService
{
    private readonly IAttachmentsService<Issue> _attachmentsService;
    private readonly IAuthorizationGuard _authGuard;
    private readonly IIssuesRepository _issuesRepository;
    private readonly ILogger<IssuesService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _user;

    public IssuesService(IIssuesRepository issuesRepository, IUnitOfWork unitOfWork,
        ICurrentUser user, IAuthorizationGuard authGuard, ILogger<IssuesService> logger,
        IAttachmentsService<Issue> attachmentsService)
    {
        _issuesRepository = issuesRepository;
        _unitOfWork = unitOfWork;
        _user = user;
        _authGuard = authGuard;
        _logger = logger;
        _attachmentsService = attachmentsService;
    }

    public Task<List<IssueDto>> GetAll(IssueQuery issueQuery) =>
        _issuesRepository.GetAllAsync(issueQuery);

    public Task<List<IssueDto>> GetCurrentUserIssues(IssueQuery issueQuery) =>
        _issuesRepository.GetAllByAuthorAsync(_user.Id, issueQuery);

    public async Task<IssueDto> GetById(int id) =>
        (await _issuesRepository.GetByIdAsync(id)).ToDto();

    public async Task<IssueDto> Create(CreateIssueRequest request)
    {
        var issue = new Issue
        {
            Title = request.Title,
            Description = request.Description,
            AuthorId = _user.Id
        };

        _issuesRepository.Add(issue);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("User {userId} created issue {issueId}", _user.Id, issue.Id);

        await _issuesRepository.LoadAuthorAsync(issue);
        return issue.ToDto();
    }

    public async Task<IssueDto> Update(int id, UpdateIssueRequest request)
    {
        var issue = await _issuesRepository.GetByIdAsync(id);
        await _authGuard.AuthorizeOwnerOrTechnician(issue);

        issue.Title = request.Title;
        issue.Description = request.Description;

        await _unitOfWork.SaveChangesAsync();
        return issue.ToDto();
    }

    public async Task Delete(int id)
    {
        var issue = await _issuesRepository.GetByIdAsync(id);
        await _authGuard.AuthorizeOwnerOrTechnician(issue);

        await _attachmentsService.DeleteAll(id);

        _issuesRepository.Remove(issue);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("User {userId} deleted issue {issueId}", _user.Id, id);
    }
}