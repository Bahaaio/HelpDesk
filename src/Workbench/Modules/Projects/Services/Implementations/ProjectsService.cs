using Workbench.Data.Persistence;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Authorization.Extensions;
using Workbench.Modules.Authorization.Services;
using Workbench.Modules.Projects.Dtos;
using Workbench.Modules.Projects.Dtos.Requests;
using Workbench.Modules.Projects.Mappers;
using Workbench.Modules.Projects.Models;
using Workbench.Modules.Projects.Repositories;

namespace Workbench.Modules.Projects.Services.Implementations;

public class ProjectsService : IProjectsService
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly IProjectsRepository _projectsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _user;

    public ProjectsService(IProjectsRepository projectsRepository, ICurrentUser user,
        IUnitOfWork unitOfWork, IAuthorizationGuard authGuard)
    {
        _projectsRepository = projectsRepository;
        _user = user;
        _unitOfWork = unitOfWork;
        _authGuard = authGuard;
    }

    public Task<List<ProjectDto>> GetAll() => _projectsRepository.GetAllAsync();

    public Task<List<ProjectDto>> GetCurrentUserProjects() =>
        _projectsRepository.GetAllByUserIdAsync(_user.Id);

    public async Task<ProjectDto> GetById(int id) =>
        (await _projectsRepository.GetByIdAsync(id)).ToDto();

    public async Task<ProjectDto> Create(CreateProjectRequest request)
    {
        var project = new Project
        {
            OwnerId = _user.Id,
            Name = request.Name,
            Description = request.Description
        };

        _projectsRepository.Add(project);
        await _unitOfWork.SaveChangesAsync();

        await _projectsRepository.LoadOwnerAsync(project);

        return project.ToDto();
    }

    public async Task<ProjectDto> Update(int id, UpdateProjectRequest request)
    {
        var project = await _projectsRepository.GetByIdAsync(id);
        // TODO: change to authorize only owner
        await _authGuard.AuthorizeOwnerOrTechnician(project);

        project.Name = request.Name;
        if (request.Description is not null) project.Description = request.Description;

        await _unitOfWork.SaveChangesAsync();

        return project.ToDto();
    }

    public async Task Delete(int id)
    {
        var project = await _projectsRepository.GetByIdAsync(id);
        // TODO: change to authorize only owner
        await _authGuard.AuthorizeOwnerOrTechnician(project);

        _projectsRepository.Remove(project);
        await _unitOfWork.SaveChangesAsync();
    }
}