using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Projects.Invites.Dtos;
using Workbench.Modules.Projects.Invites.Dtos.Requests;
using Workbench.Modules.Projects.Invites.Services;

namespace Workbench.Modules.Projects.Invites.Controllers;

[ApiController]
[Route("api/projects/{projectId:int}/invites")]
public class ProjectInvitesController : ControllerBase
{
    private readonly IProjectInvitesService _projectInvitesService;

    public ProjectInvitesController(IProjectInvitesService projectInvitesService)
    {
        _projectInvitesService = projectInvitesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<InviteDto>>> GetAllActive(int projectId) =>
        Ok(await _projectInvitesService.GetActive(projectId));

    [HttpPost]
    public async Task<ActionResult<InviteDto>> Create(int projectId, CreateInviteRequest request)
    {
        var invite = await _projectInvitesService.Create(request);
        return Created((string?)null, invite);
    }

    [HttpDelete("{code}")]
    public async Task<ActionResult> Revoke(int projectId, string code)
    {
        await _projectInvitesService.Revoke(code);
        return NoContent();
    }
}