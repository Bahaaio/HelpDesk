using Workbench.Data.Persistence;
using Workbench.Modules.Kanban.Models;

namespace Workbench.Modules.Kanban.Repositories;

public interface IBoardColumnsRepository : IRepository<BoardColumn, int>
{
}
