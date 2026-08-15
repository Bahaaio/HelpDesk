using HelpDesk.Api.Authorization;

namespace HelpDesk.Api.Models;

public class Attachment : IOwnedByUser
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string ContentType { get; set; }
    public required string OriginalFileName { get; set; }

    public required int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public required int UploaderId { get; set; }
    public ApplicationUser Uploader { get; set; } = null!;

    public int OwnerId => UploaderId;
}