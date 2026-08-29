using Workbench.Modules.Auth.Services;
using Workbench.Modules.Projects.Memberships.Dtos;
using Workbench.Modules.Projects.Memberships.Mappers;
using Workbench.Modules.Projects.Memberships.Repositories;

namespace Workbench.Modules.Projects.Memberships.Services.Implementations;

public class ProjectMembershipsService : IProjectMembershipsService
{
    private readonly IProjectMembershipsRepository _projectMembershipsRepository;
    private readonly ICurrentUser _user;

    public ProjectMembershipsService(IProjectMembershipsRepository projectMembershipsRepository,
        ICurrentUser user)
    {
        _projectMembershipsRepository = projectMembershipsRepository;
        _user = user;
    }

    public async Task<ProjectMembershipDto?> GetCurrentUserProjectMembership(int projectId) =>
        (await _projectMembershipsRepository.GetMembershipByProjectIdAndUserId(projectId, _user.Id))
        ?.ToDto();

    public Task<List<ProjectMembershipDto>> GetProjectMemberships(int projectId) =>
        _projectMembershipsRepository.GetMembershipsByProjectId(projectId);
}