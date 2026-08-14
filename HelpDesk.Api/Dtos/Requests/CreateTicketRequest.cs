using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Dtos.Requests;

public record CreateTicketRequest
{
    [Required] [MaxLength(200)] public required string Title { get; set; }
    [MaxLength(2000)] public string? Description { get; set; }
}