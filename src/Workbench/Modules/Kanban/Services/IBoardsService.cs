using Workbench.Modules.Kanban.Dtos;

namespace Workbench.Modules.Kanban.Services;

public interface IBoardsService
{
    Task<BoardDto> Get(int projectId);
    Task CreateEmpty(int projectId);
}