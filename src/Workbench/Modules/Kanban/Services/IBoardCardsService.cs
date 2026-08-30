using Workbench.Modules.Kanban.Dtos;
using Workbench.Modules.Kanban.Dtos.Requests;

namespace Workbench.Modules.Kanban.Services;

public interface IBoardCardsService
{
    Task<CardDto> Add(int projectId, CreateCardRequest request);
    Task<CardDto> Move(int projectId, int cardId, MoveCardRequest request);
    Task Delete(int projectId, int cardId);
    Task Reorder(int projectId, int columnId, List<int> cardIds);
}
