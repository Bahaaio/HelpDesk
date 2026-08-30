using Microsoft.EntityFrameworkCore;
using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Kanban.Models;

namespace Workbench.Modules.Kanban.Repositories.Implementations;

public class BoardCardsRepository : Repository<BoardCard, int>, IBoardCardsRepository
{
    private readonly AppDbContext _context;

    public BoardCardsRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Task LoadIssueAsync(BoardCard card) =>
        _context.Entry(card).Reference(c => c.Issue).LoadAsync();
}
