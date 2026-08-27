using Workbench.Modules.Attachments.Models;

namespace Workbench.Modules.Comments.Models;

public class CommentAttachment : Attachment, IHasParent<Comment>
{
    /// <summary>
    ///     Comment ID.
    /// </summary>
    public int ParentId { get; set; }

    public Comment Comment { get; set; } = null!;
}
