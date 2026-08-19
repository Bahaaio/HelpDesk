using HelpDesk.Models.Enums;

namespace HelpDesk.Models;

public class TicketStatusChange
{
    public int Id { get; set; }
    public required Status FromStatus { get; set; }
    public required Status ToStatus { get; set; }
    public DateTime ChangedAt { get; set; }

    public required int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public required int ChangedByUserId { get; set; }
    public ApplicationUser ChangedByUser { get; set; } = null!;
}
