using Workbench.Modules.Projects.Enums;
using Workbench.Modules.Projects.Memberships.Dtos;

namespace Workbench.Modules.Projects.Memberships.Services;

public interface IProjectMembershipsService
{
    Task<ProjectMembershipDto?> GetCurrentUserProjectMembership(int projectId);
    Task<List<ProjectMembershipDto>> GetProjectMemberships(int projectId);
    Task<bool> IsMember(int projectId, int userId);
    Task AddMember(int projectId, int userId, ProjectMemberRole role);
}