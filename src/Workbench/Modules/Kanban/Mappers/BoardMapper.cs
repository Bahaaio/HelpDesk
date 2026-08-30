using System.Linq.Expressions;
using Workbench.Modules.Kanban.Dtos;
using Workbench.Modules.Kanban.Models;

namespace Workbench.Modules.Kanban.Mappers;

public static class BoardMapper
{
    private static readonly Func<Board, BoardDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Board, BoardDto>> ToDtoExpression => b => new BoardDto
    {
        Id = b.Id,
        Name = b.Name,
        Columns = b.Columns
            .AsQueryable()
            .OrderBy(c => c.Position)
            .Select(ColumnMapper.ToDtoExpression)
            .ToList()
    };

    public static BoardDto ToDto(this Board board) => Compiled(board);
}