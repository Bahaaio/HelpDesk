using System.Linq.Expressions;
using Workbench.Modules.Attachments.Mappers;
using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Issues.Models;

namespace Workbench.Modules.Issues.Mappers;

public static class IssueMapper
{
    private static readonly Func<Issue, IssueDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Issue, IssueDto>> ToDtoExpression => t => new IssueDto
    {
        Id = t.Id,
        ProjectId = t.ProjectId,
        ProjectName = t.Project.Name,
        Title = t.Title,
        Description = t.Description,
        Status = t.Status,
        CreatedAt = t.CreatedAt,
        AuthorUsername = t.Author.UserName!,
        AssignedToUsername = t.AssignedTo != null ? t.AssignedTo.UserName! : null,
        Tags = t.Tags.Select(tag => tag.Name).ToList(),
        Attachments = t.Attachments.Select(ia => ia.ToDto()).ToList(),
        VoteScore = t.Votes.Sum(v => (int)v.Value)
    };

    public static IssueDto ToDto(this Issue t) => Compiled(t);
}