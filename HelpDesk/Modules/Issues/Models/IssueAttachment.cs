using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments.Services;

namespace HelpDesk.Modules.Issues.Models;

public class IssueAttachment : Attachment, IHasParent<Issue>
{
    /// <summary>
    ///     Issue ID.
    /// </summary>
    public int ParentId { get; set; }

    public Issue Issue { get; set; } = null!;
}