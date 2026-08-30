using System.Linq.Expressions;
using Workbench.Modules.Kanban.Dtos;
using Workbench.Modules.Kanban.Models;

namespace Workbench.Modules.Kanban.Mappers;

public static class CardMapper
{
    private static readonly Func<BoardCard, CardDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<BoardCard, CardDto>> ToDtoExpression => c => new CardDto
    {
        Id = c.Id,
        Position = c.Position,
        IssueId = c.IssueId,
        IssueTitle = c.Issue.Title,
        IssueStatus = c.Issue.Status
    };

    public static CardDto ToDto(this BoardCard card) => Compiled(card);
}