using HelpDesk.Common.Authorization;
using HelpDesk.Modules.Issues.Models;
using HelpDesk.Modules.Users.Models;

namespace HelpDesk.Modules.Issues.Votes;

public class Vote : IOwnedByUser
{
    public int OwnerId => VoterId;
    public required VoteValue Value { get; set; } // 1 or -1
    public DateTime CreatedAt { get; set; }

    public required int VoterId { get; set; }
    public ApplicationUser Voter { get; set; } = null!;

    public required int IssueId { get; set; }
    public Issue Issue { get; set; } = null!;
}