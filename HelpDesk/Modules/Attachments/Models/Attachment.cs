using HelpDesk.Common.Models;
using HelpDesk.Modules.Auth.Models;
using HelpDesk.Modules.Authorization.Models;

namespace HelpDesk.Modules.Attachments.Models;

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