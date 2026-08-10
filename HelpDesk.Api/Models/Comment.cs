namespace HelpDesk.Api.Models;

public class Comment
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public int AuthorId { get; set; }
    public ApplicationUser Author { get; set; } = null!;

    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;
}