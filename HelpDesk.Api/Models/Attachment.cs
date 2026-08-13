namespace HelpDesk.Api.Models;

public class Attachment
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public required int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public required int UploaderId { get; set; }
    public ApplicationUser Uploader { get; set; } = null!;
}