namespace HelpDesk.Api.Models;

public class Attachment
{
    public Guid Id { get; set; }
    public required string FilePath { get; set; }
    public DateTime CreatedAt { get; set; }

    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public int UploaderId { get; set; }
    public ApplicationUser Uploader { get; set; } = null!;
}