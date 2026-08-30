using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Kanban.Dtos;
using Workbench.Modules.Kanban.Dtos.Requests;
using Workbench.Modules.Kanban.Services;

namespace Workbench.Modules.Kanban.Controllers;

[ApiController]
[Route("api/projects/{projectId:int}/board/cards")]
public class BoardCardsController : ControllerBase
{
    private readonly IBoardCardsService _cardsService;

    public BoardCardsController(IBoardCardsService cardsService)
    {
        _cardsService = cardsService;
    }

    [HttpPost]
    public async Task<ActionResult<CardDto>> Add(int projectId, CreateCardRequest request)
    {
        var card = await _cardsService.Add(projectId, request);
        return Created((string?)null, card);
    }

    [HttpPut("{cardId}/move")]
    public async Task<ActionResult<CardDto>> Move(int projectId, int cardId,
        MoveCardRequest request) =>
        Ok(await _cardsService.Move(projectId, cardId, request));

    [HttpDelete("{cardId}")]
    public async Task<IActionResult> Delete(int projectId, int cardId)
    {
        await _cardsService.Delete(projectId, cardId);
        return NoContent();
    }

    [HttpPut("columns/{columnId}/reorder")]
    public async Task<IActionResult> Reorder(int projectId, int columnId, List<int> cardIds)
    {
        await _cardsService.Reorder(projectId, columnId, cardIds);
        return NoContent();
    }
}