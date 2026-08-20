using HelpDesk.Authorization;
using HelpDesk.Models.Enums;

namespace HelpDesk.Models;

public class Ticket : IOwnedByUser, IEntity<int>
{
    public int Id { get; set; }
    public int OwnerId => AuthorId;

    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public required int AuthorId { get; set; }
    public ApplicationUser Author { get; set; } = null!;

    public int? AssignedToId { get; set; }
    public ApplicationUser? AssignedTo { get; set; }

    public ICollection<TicketAttachment> Attachments { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Vote> Votes { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
    public ICollection<TicketStatusChange> StatusChanges { get; set; } = [];
}