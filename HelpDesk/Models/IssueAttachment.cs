namespace HelpDesk.Models;

public class IssueAttachment : Attachment, IHasParent<Issue>
{
    /// <summary>
    ///     Issue ID.
    /// </summary>
    public int ParentId { get; set; }

    public Issue Issue { get; set; } = null!;
}