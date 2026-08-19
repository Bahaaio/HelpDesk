namespace HelpDesk.Models;

public class TicketAttachment : IAttachmentJoin<Ticket>
{
    public Ticket Ticket { get; set; } = null!;
    public int OwnerId { get; set; }

    public Guid AttachmentId { get; set; }
    public Attachment Attachment { get; set; } = null!;
}
