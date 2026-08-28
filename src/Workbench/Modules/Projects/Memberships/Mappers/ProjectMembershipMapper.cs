using System.Linq.Expressions;
using Workbench.Modules.Projects.Memberships.Dtos;
using Workbench.Modules.Projects.Memberships.Models;

namespace Workbench.Modules.Projects.Memberships.Mappers;

public static class ProjectMembershipMapper
{
    private static readonly Func<ProjectMembership, ProjectMembershipDto> Compiled =
        ToDtoExpression.Compile();

    public static Expression<Func<ProjectMembership, ProjectMembershipDto>> ToDtoExpression =>
        pm => new ProjectMembershipDto(pm.UserId, pm.User.UserName!, pm.Role);

    public static ProjectMembershipDto ToDto(this ProjectMembership membership) =>
        Compiled(membership);
}