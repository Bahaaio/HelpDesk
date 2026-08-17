using HelpDesk.Api.Authorization;

namespace HelpDesk.Api.Models;

public abstract class Attachment : IOwnedByUser
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string ContentType { get; set; }
    public required string OriginalFileName { get; set; }

    public required int UploaderId { get; set; }
    public ApplicationUser Uploader { get; set; } = null!;

    public abstract int ResourceId { get; }

    public int OwnerId => UploaderId;
}