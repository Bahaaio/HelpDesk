using HelpDesk.Api.Models.Enums;

namespace HelpDesk.Api.Models;

public class Ticket
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int AuthorId { get; set; }
    public ApplicationUser Author { get; set; } = null!;
}