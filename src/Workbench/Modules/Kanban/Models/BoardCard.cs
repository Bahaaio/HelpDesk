using Workbench.Common.Models;
using Workbench.Modules.Issues.Models;

namespace Workbench.Modules.Kanban.Models;

public class BoardCard : IEntity<int>
{
    public int Id { get; set; }

    /// <summary>
    ///     The position of the card in the column. Lower numbers are higher up in the column.
    /// </summary>
    public required int Position { get; set; }

    /// <summary>
    ///     Denormalized to enforce that a card can only exist in one board and one column at a time.
    /// </summary>
    public required int BoardId { get; set; }

    public required int ColumnId { get; set; }
    public BoardColumn Column { get; set; } = null!;

    public required int IssueId { get; set; }
    public Issue Issue { get; set; } = null!;
}