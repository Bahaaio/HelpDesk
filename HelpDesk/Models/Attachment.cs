using HelpDesk.Authorization;

namespace HelpDesk.Models;

/// <summary>
///     Base class for all attachments.
/// </summary>
public abstract class Attachment : IOwnedByUser, IEntity<Guid>
{
    public Guid Id { get; set; }
    public int OwnerId => UploaderId;
    public DateTime CreatedAt { get; set; }
    public string ContentType { get; set; }
    public string OriginalFileName { get; set; }

    public int UploaderId { get; set; }
    public ApplicationUser Uploader { get; set; } = null!;
}