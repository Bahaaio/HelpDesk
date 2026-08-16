using HelpDesk.Api.Authorization;
using HelpDesk.Api.Models.Enums;

namespace HelpDesk.Api.Models;

public class Ticket : IOwnedByUser
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public required int AuthorId { get; set; }
    public ApplicationUser Author { get; set; } = null!;

    public int? AssignedToId { get; set; }
    public ApplicationUser? AssignedTo { get; set; }

    public ICollection<Attachment> Attachments { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Vote> Votes { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];

    public int OwnerId => AuthorId;
}