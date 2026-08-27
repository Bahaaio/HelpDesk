using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Issues.Dtos.Requests;

public record UpdateIssueRequest
{
    [Required] [MaxLength(200)] public required string Title { get; set; }
    [MaxLength(2000)] public string? Description { get; set; }
}
