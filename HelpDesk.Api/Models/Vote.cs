using HelpDesk.Api.Models.Enums;

namespace HelpDesk.Api.Models;

public class Vote
{
    public int Id { get; set; }
    public VoteValue Value { get; set; } // 1 or -1
    public DateTime CreatedAt { get; set; }

    public int VoterId { get; set; }
    public ApplicationUser Voter { get; set; } = null!;

    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;
}