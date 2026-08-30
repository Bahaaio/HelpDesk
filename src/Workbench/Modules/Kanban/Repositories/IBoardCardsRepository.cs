using Workbench.Data.Persistence;
using Workbench.Modules.Kanban.Models;

namespace Workbench.Modules.Kanban.Repositories;

public interface IBoardCardsRepository : IRepository<BoardCard, int>
{
    Task LoadIssueAsync(BoardCard card);
}
