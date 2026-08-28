using Workbench.Modules.Projects.Dtos;
using Workbench.Modules.Projects.Dtos.Requests;

namespace Workbench.Modules.Projects.Services;

public interface IProjectsService
{
    /// <summary>
    ///     Gets all projects in the system.
    /// </summary>
    Task<List<ProjectDto>> GetAll();

    /// <summary>
    ///     Gets all projects owned by the current user.
    /// </summary>
    /// <returns></returns>
    Task<List<ProjectDto>> GetCurrentUserProjects();

    /// <summary>
    ///     Gets a single project by its ID.
    /// </summary>
    /// <param name="id">The project ID.</param>
    Task<ProjectDto> GetById(int id);

    /// <summary>
    ///     Creates a new project owned by the current user.
    /// </summary>
    /// <param name="request">The project name and optional description.</param>
    Task<ProjectDto> Create(CreateProjectRequest request);

    /// <summary>
    ///     Updates an existing project. Only the project owner may update.
    /// </summary>
    /// <param name="id">The project ID.</param>
    /// <param name="request">The updated project name and optional description.</param>
    Task<ProjectDto> Update(int id, UpdateProjectRequest request);

    /// <summary>
    ///     Deletes a project. Only the project owner may delete.
    /// </summary>
    /// <param name="id">The project ID.</param>
    Task Delete(int id);
}