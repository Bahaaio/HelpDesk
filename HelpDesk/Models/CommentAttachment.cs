namespace HelpDesk.Models;

public class CommentAttachment : IAttachmentJoin<Comment>
{
    public Guid AttachmentId { get; set; }
    public Attachment Attachment { get; set; }

    public int OwnerId { get; set; }
    public Comment Comment { get; set; }
}