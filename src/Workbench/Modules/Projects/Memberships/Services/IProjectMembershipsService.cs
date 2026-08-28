using Workbench.Modules.Projects.Memberships.Dtos;

namespace Workbench.Modules.Projects.Memberships.Services;

public interface IProjectMembershipsService
{
    Task<ProjectMembershipDto?> GetCurrentUserProjectMembership(int projectId);
    Task<List<ProjectMembershipDto>> GetProjectMemberships(int projectId);
}