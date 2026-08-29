using Workbench.Common.Models;
using Workbench.Modules.Auth.Models;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Comments.Models;
using Workbench.Modules.Issues.Enums;
using Workbench.Modules.Issues.Votes.Models;
using Workbench.Modules.Projects.Models;
using Workbench.Modules.Tags.Models;

namespace Workbench.Modules.Issues.Models;

public class Issue : IEntity<int>, IOwnedByUser, IBelongsToProject
{
    public int Id { get; set; }
    public int OwnerId => AuthorId;

    public required int ProjectId { get; set; }

    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public Project Project { get; set; } = null!;

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