using Workbench.Modules.Issues.Models;

namespace Workbench.Modules.Milestones.Models;

public class MilestoneItem
{
    public required int MilestoneId { get; set; }
    public Milestone Milestone { get; set; } = null!;

    public required int IssueId { get; set; }
    public Issue Issue { get; set; } = null!;
}