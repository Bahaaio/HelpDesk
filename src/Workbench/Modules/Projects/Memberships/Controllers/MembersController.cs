using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Projects.Memberships.Dtos;
using Workbench.Modules.Projects.Memberships.Services;

namespace Workbench.Modules.Projects.Memberships.Controllers;

[ApiController]
[Route("api/projects/{projectId:int}/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IProjectMembershipsService _membershipsService;

    public MembersController(IProjectMembershipsService membershipsService)
    {
        _membershipsService = membershipsService;
    }

    [HttpGet("me")]
    public async Task<ActionResult<ProjectMembershipDto>> GetCurrentUserMembership(int projectId)
    {
        var membership = await _membershipsService.GetCurrentUserProjectMembership(projectId);
        return membership is not null ? Ok(membership) : NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectMembershipDto>>> GetProjectMemberships(int projectId) =>
        Ok(await _membershipsService.GetProjectMemberships(projectId));
}
