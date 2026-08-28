using Workbench.Modules.Projects.Memberships.Dtos;
using Workbench.Modules.Projects.Memberships.Models;

namespace Workbench.Modules.Projects.Memberships.Repositories;

public interface IProjectMembershipsRepository
{
    Task<ProjectMembership?> GetMembershipByProjectIdAndUserId(int projectId, int userId);
    Task<List<ProjectMembershipDto>> GetMembershipsByProjectId(int projectId);
    void Remove(ProjectMembership membership);
}