using System.Linq.Expressions;
using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Models;

using HelpDesk.Modules.Attachments.Mappers;

namespace HelpDesk.Modules.Issues.Mappers;

public static class IssueMapper
{
    private static readonly Func<Issue, IssueDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Issue, IssueDto>> ToDtoExpression => t => new IssueDto
    {
        Id = t.Id,
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