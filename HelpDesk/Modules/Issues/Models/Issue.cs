using HelpDesk.Common.Authorization;
using HelpDesk.Common.Entities;
using HelpDesk.Modules.Comments.Models;
using HelpDesk.Modules.Issues.Enums;
using HelpDesk.Modules.Tags;
using HelpDesk.Modules.Users;
using HelpDesk.Modules.Issues.Votes;

namespace HelpDesk.Modules.Issues.Models;

public class Issue : IOwnedByUser, IEntity<int>
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

    public ICollection<IssueAttachment> Attachments { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Vote> Votes { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
    public ICollection<IssueStatusChange> StatusChanges { get; set; } = [];
}