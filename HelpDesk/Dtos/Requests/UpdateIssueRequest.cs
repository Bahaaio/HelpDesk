using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Dtos.Requests;

public record UpdateIssueRequest
{
    [Required] [MaxLength(200)] public required string Title { get; set; }
    [MaxLength(2000)] public string? Description { get; set; }
}
