using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Projects.Dtos;
using Workbench.Modules.Projects.Dtos.Requests;
using Workbench.Modules.Projects.Services;

namespace Workbench.Modules.Projects.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService _projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> GetAll() =>
        Ok(await _projectsService.GetAll());

    [HttpGet("mine")]
    public async Task<ActionResult<List<ProjectDto>>> GetCurrentUserProjects() =>
        Ok(await _projectsService.GetCurrentUserProjects());
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetById(int id) =>
        Ok(await _projectsService.GetById(id));

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> Create(CreateProjectRequest request)
    {
        var project = await _projectsService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectDto>> Update(int id, UpdateProjectRequest request) =>
        Ok(await _projectsService.Update(id, request));
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _projectsService.Delete(id);
        return NoContent();
    }
}