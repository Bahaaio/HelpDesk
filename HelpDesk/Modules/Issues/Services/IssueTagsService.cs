using HelpDesk.Common.Exceptions;
using HelpDesk.Data;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Issues.Services;

public class IssueTagsService : IIssueTagsService
{
    private readonly AppDbContext _db;

    public IssueTagsService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<string>> UpdateTags(int issueId, List<string> tags)
    {
        var issue = await _db.Issues
            .Include(t => t.Tags)
            .SingleOrDefaultAsync(t => t.Id == issueId);

        if (issue is null)
            throw new NotFoundException($"Issue with id {issueId} not found");

        var lowerTags = tags.Select(n => n.ToLower());

        var tagEntities = await _db.Tags
            .Where(t => lowerTags.Contains(t.Name))
            .ToListAsync();

        var missing = lowerTags.Except(tagEntities.Select(t => t.Name)).ToList();
        if (missing.Count != 0)
            throw new NotFoundException($"Tags {string.Join(", ", missing)} not found");

        issue.Tags = tagEntities;
        await _db.SaveChangesAsync();

        return issue.Tags.Select(t => t.Name).ToList();
    }
}