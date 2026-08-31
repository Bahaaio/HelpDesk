using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Projects.Invites.Services;

namespace Workbench.Modules.Projects.Invites.Controllers;

[ApiController]
[Route("api/projects/{projectId}/join")]
public class ProjectJoinController : ControllerBase
{
    private readonly IProjectInvitesService _projectInvitesService;

    public ProjectJoinController(IProjectInvitesService projectInvitesService)
    {
        _projectInvitesService = projectInvitesService;
    }

    [HttpPost("{code}")]
    public async Task<ActionResult> Join(int projectId, string code)
    {
        await _projectInvitesService.Consume(code);
        return NoContent();
    }
}