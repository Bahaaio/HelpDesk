using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Projects.Dtos.Requests;

public record UpdateProjectRequest
{
    [Required] [MaxLength(100)] public required string Name { get; set; }
    [MaxLength(500)] public string? Description { get; set; }
}