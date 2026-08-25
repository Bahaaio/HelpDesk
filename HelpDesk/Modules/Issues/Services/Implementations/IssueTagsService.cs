using HelpDesk.Common.Exceptions;
using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Issues.Repositories;
using HelpDesk.Modules.Tags.Repositories;

namespace HelpDesk.Modules.Issues.Services.Implementations;

public class IssueTagsService : IIssueTagsService
{
    private readonly IIssuesRepository _issuesRepository;
    private readonly ITagsRepository _tagsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public IssueTagsService(IIssuesRepository issuesRepository, ITagsRepository tagsRepository,
        IUnitOfWork unitOfWork)
    {
        _issuesRepository = issuesRepository;
        _tagsRepository = tagsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<string>> UpdateTags(int issueId, List<string> tags)
    {
        var issue = await _issuesRepository.FindWithTagsAsync(issueId)
                    ?? throw new NotFoundException($"Issue with id {issueId} not found");

        var lowerTags = tags.Select(n => n.ToLower()).ToList();

        var tagEntities = await _tagsRepository.GetByNamesAsync(lowerTags);

        var missing = lowerTags.Except(tagEntities.Select(t => t.Name)).ToList();
        if (missing.Count != 0)
            throw new NotFoundException($"Tags {string.Join(", ", missing)} not found");

        issue.Tags = tagEntities;
        await _unitOfWork.SaveChangesAsync();

        return issue.Tags.Select(t => t.Name).ToList();
    }
}