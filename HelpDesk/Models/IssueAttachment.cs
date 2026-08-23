namespace HelpDesk.Models;

public class IssueAttachment : IAttachmentJoin<Issue>
{
    public Issue Issue { get; set; } = null!;
    public int OwnerId { get; set; }

    public Guid AttachmentId { get; set; }
    public Attachment Attachment { get; set; } = null!;
}
