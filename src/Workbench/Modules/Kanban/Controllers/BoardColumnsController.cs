using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Kanban.Dtos;
using Workbench.Modules.Kanban.Dtos.Requests;
using Workbench.Modules.Kanban.Services;

namespace Workbench.Modules.Kanban.Controllers;

[ApiController]
[Route("api/projects/{projectId:int}/board/columns")]
public class BoardColumnsController : ControllerBase
{
    private readonly IBoardColumnsService _columnsService;

    public BoardColumnsController(IBoardColumnsService columnsService)
    {
        _columnsService = columnsService;
    }

    [HttpPost]
    public async Task<ActionResult<ColumnDto>> Add(int projectId, CreateColumnRequest request)
    {
        var column = await _columnsService.Add(projectId, request);
        return CreatedAtAction(nameof(Add), new { projectId, columnId = column.Id }, column);
    }

    [HttpPut("{columnId}")]
    public async Task<ActionResult<ColumnDto>> Update(int projectId, int columnId,
        UpdateColumnRequest request) =>
        Ok(await _columnsService.Update(projectId, columnId, request));

    [HttpDelete("{columnId}")]
    public async Task<IActionResult> Delete(int projectId, int columnId)
    {
        await _columnsService.Delete(projectId, columnId);
        return NoContent();
    }

    [HttpPut("reorder")]
    public async Task<IActionResult> Reorder(int projectId, MoveColumnRequest request)
    {
        await _columnsService.Reorder(projectId, request);
        return NoContent();
    }
}