using HelpDesk.Authorization;

namespace HelpDesk.Models;

public class Attachment : IOwnedByUser
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string ContentType { get; set; }
    public required string OriginalFileName { get; set; }

    public required int UploaderId { get; set; }
    public ApplicationUser Uploader { get; set; } = null!;

    public int OwnerId => UploaderId;
}
