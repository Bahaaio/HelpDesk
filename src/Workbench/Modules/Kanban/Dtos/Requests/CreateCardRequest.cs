using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Kanban.Dtos.Requests;

public record CreateCardRequest
{
    [Required] public required int IssueId { get; set; }
    [Required] public required int ColumnId { get; set; }
}