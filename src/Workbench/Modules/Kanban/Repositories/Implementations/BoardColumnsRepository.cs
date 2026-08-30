using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Kanban.Models;

namespace Workbench.Modules.Kanban.Repositories.Implementations;

public class BoardColumnsRepository : Repository<BoardColumn, int>, IBoardColumnsRepository
{
    public BoardColumnsRepository(AppDbContext context) : base(context)
    {
    }
}
