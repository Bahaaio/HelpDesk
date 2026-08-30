using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Kanban.Dtos;
using Workbench.Modules.Kanban.Services;

namespace Workbench.Modules.Kanban.Controllers;

[ApiController]
[Route("api/projects/{projectId:int}/board")]
public class BoardsController : ControllerBase
{
    private readonly IBoardsService _boardsService;

    public BoardsController(IBoardsService boardsService)
    {
        _boardsService = boardsService;
    }

    [HttpGet]
    public async Task<ActionResult<BoardDto>> Get(int projectId) =>
        Ok(await _boardsService.Get(projectId));
}
