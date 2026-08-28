using System.Linq.Expressions;
using Workbench.Modules.Projects.Dtos;
using Workbench.Modules.Projects.Models;

namespace Workbench.Modules.Projects.Mappers;

public static class ProjectMapper
{
    private static readonly Func<Project, ProjectDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Project, ProjectDto>> ToDtoExpression => p => new ProjectDto
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        CreatedAt = p.CreatedAt,
        OwnerUsername = p.Owner.UserName!
    };

    public static ProjectDto ToDto(this Project p) => Compiled(p);
}