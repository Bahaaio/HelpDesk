using Workbench.Data.Persistence;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Projects.Enums;
using Workbench.Modules.Projects.Memberships.Dtos;
using Workbench.Modules.Projects.Memberships.Mappers;
using Workbench.Modules.Projects.Memberships.Models;
using Workbench.Modules.Projects.Memberships.Repositories;

namespace Workbench.Modules.Projects.Memberships.Services.Implementations;

public class ProjectMembershipsService : IProjectMembershipsService
{
    private readonly IProjectMembershipsRepository _projectMembershipsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _user;

    public ProjectMembershipsService(IProjectMembershipsRepository projectMembershipsRepository,
        IUnitOfWork unitOfWork, ICurrentUser user)
    {
        _projectMembershipsRepository = projectMembershipsRepository;
        _unitOfWork = unitOfWork;
        _user = user;
    }

    public async Task<ProjectMembershipDto?> GetCurrentUserProjectMembership(int projectId) =>
        (await _projectMembershipsRepository.GetMembershipByProjectIdAndUserId(projectId, _user.Id))
        ?.ToDto();

    public Task<List<ProjectMembershipDto>> GetProjectMemberships(int projectId) =>
        _projectMembershipsRepository.GetMembershipsByProjectId(projectId);

    public async Task<bool> IsMember(int projectId, int userId) =>
        await _projectMembershipsRepository
            .GetMembershipByProjectIdAndUserId(projectId, userId) is not null;

    public async Task AddMember(int projectId, int userId, ProjectMemberRole role)
    {
        _projectMembershipsRepository.Add(new ProjectMembership
        {
            ProjectId = projectId,
            UserId = userId,
            Role = role
        });

        await _unitOfWork.SaveChangesAsync();
    }
}