using System.Linq.Expressions;
using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Issues.Models;

namespace Workbench.Modules.Issues.Mappers;

public static class StatusChangeMapper
{
    private static readonly Func<IssueStatusChange, StatusChangeDto> Compiled =
        ToDtoExpression.Compile();

    public static Expression<Func<IssueStatusChange, StatusChangeDto>> ToDtoExpression => s =>
        new StatusChangeDto
        {
            FromStatus = s.FromStatus,
            ToStatus = s.ToStatus,
            ChangedByUsername = s.ChangedByUser.UserName!,
            ChangedAt = s.ChangedAt
        };

    public static StatusChangeDto ToDto(this IssueStatusChange s) => Compiled(s);
}
