namespace HelpDesk.Models;

public class CommentAttachment : Attachment
{
    public Comment Comment { get; set; } = null!;
}
