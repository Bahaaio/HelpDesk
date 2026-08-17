namespace HelpDesk.Api.Models;

public class TicketAttachment : Attachment
{
    public required int TicketId { get; set; }
    public override int ResourceId => TicketId;
}