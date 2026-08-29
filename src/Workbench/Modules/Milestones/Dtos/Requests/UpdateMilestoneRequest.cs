using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Milestones.Dtos.Requests;

public record UpdateMilestoneRequest
{
    [Required] [MaxLength(255)] public required string Name { get; set; }
    [MaxLength(1000)] public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
}
