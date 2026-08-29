using Workbench.Common.Models;
using Workbench.Modules.Auth.Models;
using Workbench.Modules.Authorization.Models;

namespace Workbench.Modules.Attachments.Models;

/// <summary>
///     Base class for all attachments.
/// </summary>
public abstract class Attachment : IOwnedByUser, IEntity<Guid>
{
    public Guid Id { get; set; }
    public int OwnerId => UploaderId;
    public DateTime CreatedAt { get; set; }
    public string ContentType { get; set; } = null!;
    public string OriginalFileName { get; set; } = null!;

    public int UploaderId { get; set; }
    public ApplicationUser Uploader { get; set; } = null!;
}
