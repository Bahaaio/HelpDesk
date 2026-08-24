using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Modules.Issues.Dtos.Requests;

public record CreateIssueRequest
{
    [Required] [MaxLength(200)] public required string Title { get; set; }
    [MaxLength(2000)] public string? Description { get; set; }
}