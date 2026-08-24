namespace HelpDesk.Models;

public class IssueAttachment : Attachment
{
    public Issue Issue { get; set; } = null!;
}
