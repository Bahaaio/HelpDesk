using Workbench.Data.Persistence;
using Workbench.Modules.Kanban.Dtos;
using Workbench.Modules.Kanban.Models;

namespace Workbench.Modules.Kanban.Repositories;

public interface IBoardsRepository : IRepository<Board, int>
{
    Task<BoardDto> GetByProjectId(int projectId);
    Task<Board> GetByProjectIdRaw(int projectId);
}