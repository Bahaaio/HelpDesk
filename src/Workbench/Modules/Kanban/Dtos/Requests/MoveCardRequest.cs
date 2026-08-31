using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Kanban.Dtos.Requests;

public record MoveCardRequest
{
    [Required] public List<int> CardIds { get; set; }
}