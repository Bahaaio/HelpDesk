using HelpDesk.Api.Authorization;

namespace HelpDesk.Api.Models;

public class Comment : IOwnedByUser
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public required int AuthorId { get; set; }
    public ApplicationUser Author { get; set; } = null!;

    public required int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public int OwnerId => AuthorId;
}