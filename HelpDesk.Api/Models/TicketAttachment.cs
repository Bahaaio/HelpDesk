namespace HelpDesk.Api.Models;

public class TicketAttachment : IAttachmentJoin<Ticket>
{
    public Ticket Ticket { get; set; }
    public int OwnerId { get; set; }

    public Guid AttachmentId { get; set; }
    public Attachment Attachment { get; set; }
}