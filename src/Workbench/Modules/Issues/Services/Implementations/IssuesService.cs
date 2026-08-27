using Workbench.Data.Persistence;
using Workbench.Modules.Attachments.Services;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Extensions;
using Workbench.Modules.Authorization.Services;
using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Issues.Dtos.Requests;
using Workbench.Modules.Issues.Mappers;
using Workbench.Modules.Issues.Models;
using Workbench.Modules.Issues.Repositories;

namespace Workbench.Modules.Issues.Services.Implementations;

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
