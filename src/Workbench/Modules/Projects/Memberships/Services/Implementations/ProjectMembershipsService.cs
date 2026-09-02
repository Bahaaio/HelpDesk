using Workbench.Common.Exceptions;
using Workbench.Data.Persistence;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Extensions;
using Workbench.Modules.Authorization.Services;
using Workbench.Modules.Projects.Enums;
using Workbench.Modules.Projects.Memberships.Dtos;
using Workbench.Modules.Projects.Memberships.Mappers;
using Workbench.Modules.Projects.Memberships.Models;
using Workbench.Modules.Projects.Memberships.Repositories;
using Workbench.Modules.Projects.Repositories;

namespace Workbench.Modules.Projects.Memberships.Services.Implementations;

public class ProjectMembershipsService : IProjectMembershipsService
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly IProjectMembershipsRepository _projectMembershipsRepository;
    private readonly IProjectsRepository _projectsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _user;

    public ProjectMembershipsService(IProjectMembershipsRepository projectMembershipsRepository,
        IUnitOfWork unitOfWork, ICurrentUser user, IProjectsRepository projectsRepository,
        IAuthorizationGuard authGuard)
    {
        _projectMembershipsRepository = projectMembershipsRepository;
        _unitOfWork = unitOfWork;
        _user = user;
        _projectsRepository = projectsRepository;
        _authGuard = authGuard;
    }

    public async Task<ProjectMembershipDto?> GetCurrentUserProjectMembership(int projectId) =>
        (await _projectMembershipsRepository
            .FindMembershipByProjectIdAndUserId(projectId, _user.Id))
        ?.ToDto();

    public Task<List<ProjectMembershipDto>> GetProjectMemberships(int projectId) =>
        _projectMembershipsRepository.GetMembershipsByProjectId(projectId);

    public async Task<bool> IsMember(int projectId, int userId) =>
        await _projectMembershipsRepository
            .FindMembershipByProjectIdAndUserId(projectId, userId) is not null;

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

    public async Task UpdateRole(int projectId, string username, ProjectMemberRole role)
    {
        var project = await _projectsRepository.GetByIdAsync(projectId);
        await _authGuard.AuthorizeProjectLead(project);

        var membership = await _projectMembershipsRepository
            .GetByProjectIdAndUsernameAsync(projectId, username);

        if (membership.UserId == _user.Id)
            throw new BadRequestException("Cannot change your own role");

        if (membership.UserId == project.OwnerId)
            throw new BadRequestException("Cannot change the owner's role");

        membership.Role = role;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveMember(int projectId, string username)
    {
        var project = await _projectsRepository.GetByIdAsync(projectId);
        await _authGuard.AuthorizeProjectLead(project);

        var membership = await _projectMembershipsRepository
            .GetByProjectIdAndUsernameAsync(projectId, username);

        if (membership.UserId == _user.Id)
            throw new BadRequestException("Cannot remove yourself");

        if (membership.UserId == project.OwnerId)
            throw new BadRequestException("Cannot remove the project owner");

        if (membership.Role == ProjectMemberRole.Lead)
            throw new BadRequestException("Cannot remove a lead. Demote them first.");

        _projectMembershipsRepository.Remove(membership);
        await _unitOfWork.SaveChangesAsync();
    }
}