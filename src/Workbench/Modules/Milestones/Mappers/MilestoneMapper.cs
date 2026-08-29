using System.Linq.Expressions;
using Workbench.Modules.Issues.Enums;
using Workbench.Modules.Milestones.Dtos;
using Workbench.Modules.Milestones.Models;

namespace Workbench.Modules.Milestones.Mappers;

public static class MilestoneMapper
{
    private static readonly Func<Milestone, MilestoneDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Milestone, MilestoneDto>> ToDtoExpression => m => new MilestoneDto
    {
        Id = m.Id,
        ProjectId = m.ProjectId,
        Name = m.Name,
        Description = m.Description,
        DueDate = m.DueDate,
        TotalItems = m.MilestoneItems.Count,
        CompletedItems = m.MilestoneItems.Count(mi => mi.Issue.Status == Status.Closed)
    };

    public static MilestoneDto ToDto(this Milestone m) => Compiled(m);
}
