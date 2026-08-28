using Workbench.Data.Persistence;
using Workbench.Modules.Projects.Dtos;
using Workbench.Modules.Projects.Models;

namespace Workbench.Modules.Projects.Repositories;

public interface IProjectsRepository : IRepository<Project, int>
{
    Task<List<ProjectDto>> GetAllAsync();
    Task<List<ProjectDto>> GetAllByUserIdAsync(int userId);
    Task LoadOwnerAsync(Project project);
}