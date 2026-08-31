using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Kanban.Dtos.Requests;

public record MoveColumnRequest
{
    [Required] public List<int> ColumnIds { get; init; }
}