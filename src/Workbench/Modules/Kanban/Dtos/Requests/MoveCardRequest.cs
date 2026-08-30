using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Kanban.Dtos.Requests;

public record MoveCardRequest
{
    [Required] public int ColumnId { get; set; }
    public int Position { get; set; }
}
