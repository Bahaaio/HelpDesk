using Workbench.Modules.Projects.Enums;
using Workbench.Modules.Projects.Memberships.Dtos;

namespace Workbench.Modules.Projects.Memberships.Services;

public interface IProjectMembershipsService
{
    Task<ProjectMembershipDto?> FindCurrentUserProjectMembership(int projectId);
    public Task<ProjectMembershipDto> GetProjectMembership(int projectId, string username);
    Task<List<ProjectMembershipDto>> GetProjectMemberships(int projectId);
    Task<bool> IsMember(int projectId, int userId);
    Task AddMember(int projectId, int userId, ProjectMemberRole role);
    Task UpdateRole(int projectId, string username, ProjectMemberRole role);
    Task RemoveMember(int projectId, string username);
}