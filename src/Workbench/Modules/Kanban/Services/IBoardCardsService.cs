using Workbench.Modules.Kanban.Dtos;
using Workbench.Modules.Kanban.Dtos.Requests;

namespace Workbench.Modules.Kanban.Services;

public interface IBoardCardsService
{
    Task<CardDto> Add(int projectId, CreateCardRequest request);
    Task Reorder(int projectId, int columnId, MoveCardRequest request);
    Task Delete(int projectId, int cardId);
}