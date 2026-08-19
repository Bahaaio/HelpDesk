namespace HelpDesk.Models;

/// <summary>
///     Join-table between attachments and other entities.
/// </summary>
/// <typeparam name="TOwner">Used to tie the join table to the owner entity.</typeparam>
public interface IAttachmentJoin<TOwner>
{
    public Guid AttachmentId { get; set; }
    public Attachment Attachment { get; set; }

    public int OwnerId { get; set; }
}
