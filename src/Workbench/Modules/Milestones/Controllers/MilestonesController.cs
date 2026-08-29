using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Milestones.Dtos;
using Workbench.Modules.Milestones.Dtos.Requests;
using Workbench.Modules.Milestones.Services;

namespace Workbench.Modules.Milestones.Controllers;

[ApiController]
[Route("api/projects/{projectId:int}/[controller]")]
public class MilestonesController : ControllerBase
{
    private readonly IMilestonesService _milestonesService;

    public MilestonesController(IMilestonesService milestonesService)
    {
        _milestonesService = milestonesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MilestoneDto>>> GetAll(int projectId) =>
        Ok(await _milestonesService.GetAll(projectId));

    [HttpGet("{milestoneId}")]
    public async Task<ActionResult<MilestoneDto>> GetById(int projectId, int milestoneId) =>
        Ok(await _milestonesService.GetById(projectId, milestoneId));

    [HttpPost]
    public async Task<ActionResult<MilestoneDto>> Create(int projectId, CreateMilestoneRequest request)
    {
        var milestone = await _milestonesService.Create(projectId, request);
        return CreatedAtAction(nameof(GetById), new { projectId, milestoneId = milestone.Id }, milestone);
    }

    [HttpPut("{milestoneId}")]
    public async Task<ActionResult<MilestoneDto>> Update(int projectId, int milestoneId, UpdateMilestoneRequest request) =>
        Ok(await _milestonesService.Update(projectId, milestoneId, request));

    [HttpDelete("{milestoneId}")]
    public async Task<IActionResult> Delete(int projectId, int milestoneId)
    {
        await _milestonesService.Delete(projectId, milestoneId);
        return NoContent();
    }
}
