using Workbench.Modules.Issues.Votes.Enums;
using Workbench.Modules.Auth.Models;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Issues.Models;

namespace Workbench.Modules.Issues.Votes.Models;

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
