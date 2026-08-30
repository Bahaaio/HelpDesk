using Microsoft.EntityFrameworkCore;
using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Kanban.Dtos;
using Workbench.Modules.Kanban.Mappers;
using Workbench.Modules.Kanban.Models;

namespace Workbench.Modules.Kanban.Repositories.Implementations;

public class BoardsRepository : Repository<Board, int>, IBoardsRepository
{
    public BoardsRepository(AppDbContext context) : base(context)
    {
    }

    public Task<BoardDto> GetByProjectId(int projectId) =>
        DbSet
            .Where(b => b.ProjectId == projectId)
            .Select(BoardMapper.ToDtoExpression)
            .SingleAsync();

    public Task<Board> GetByProjectIdRaw(int projectId) =>
        DbSet
            .Include(b => b.Columns)
            .ThenInclude(c => c.Cards)
            .ThenInclude(c => c.Issue)
            .SingleAsync(b => b.ProjectId == projectId);
}