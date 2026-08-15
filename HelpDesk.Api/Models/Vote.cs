using HelpDesk.Api.Authorization;
using HelpDesk.Api.Models.Enums;

namespace HelpDesk.Api.Models;

public class Vote : IOwnedByUser
{
    public required VoteValue Value { get; set; } // 1 or -1
    public DateTime CreatedAt { get; set; }

    public required int VoterId { get; set; }
    public ApplicationUser Voter { get; set; } = null!;

    public required int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public int OwnerId => VoterId;
}