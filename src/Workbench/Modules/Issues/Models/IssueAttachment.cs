using Workbench.Modules.Attachments.Dtos;
using Workbench.Modules.Attachments.Models;
using Workbench.Modules.Attachments.Services;

namespace Workbench.Modules.Issues.Models;

public class IssueAttachment : Attachment, IHasParent<Issue>
{
    /// <summary>
    ///     Issue ID.
    /// </summary>
    public int ParentId { get; set; }

    public Issue Issue { get; set; } = null!;
}
