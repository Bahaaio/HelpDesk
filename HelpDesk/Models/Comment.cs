using HelpDesk.Authorization;

namespace HelpDesk.Models;

public class Comment : IOwnedByUser, IEntity<int>
{
    public int Id { get; set; }
    public int OwnerId => AuthorId;

    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public required int AuthorId { get; set; }
    public ApplicationUser Author { get; set; } = null!;

    public required int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public ICollection<CommentAttachment> Attachments { get; set; } = [];
}