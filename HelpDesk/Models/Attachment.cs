using HelpDesk.Authorization;

namespace HelpDesk.Models;

public class Attachment : IOwnedByUser, IEntity<Guid>
{
    public Guid Id { get; set; }
    public int OwnerId => UploaderId;

    public DateTime CreatedAt { get; set; }
    public required string ContentType { get; set; }
    public required string OriginalFileName { get; set; }

    public required int UploaderId { get; set; }
    public ApplicationUser Uploader { get; set; } = null!;
}