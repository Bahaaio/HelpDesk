using HelpDesk.Common.Entities;
using HelpDesk.Modules.Issues.Enums;
using HelpDesk.Modules.Users.Models;

namespace HelpDesk.Modules.Issues.Models;

public class IssueStatusChange : IEntity<int>
{
    public int Id { get; set; }
    public required Status FromStatus { get; set; }
    public required Status ToStatus { get; set; }
    public DateTime ChangedAt { get; set; }

    public required int IssueId { get; set; }
    public Issue Issue { get; set; } = null!;

    public required int ChangedByUserId { get; set; }
    public ApplicationUser ChangedByUser { get; set; } = null!;
}