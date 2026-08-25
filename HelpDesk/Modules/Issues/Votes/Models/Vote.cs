using HelpDesk.Modules.Issues.Votes.Enums;
using HelpDesk.Modules.Auth.Models;
using HelpDesk.Modules.Authorization.Models;
using HelpDesk.Modules.Issues.Models;

namespace HelpDesk.Modules.Issues.Votes.Models;

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