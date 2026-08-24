using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments.Services;

namespace HelpDesk.Modules.Comments.Models;

public class CommentAttachment : Attachment, IHasParent<Comment>
{
    /// <summary>
    ///     Comment ID.
    /// </summary>
    public int ParentId { get; set; }

    public Comment Comment { get; set; } = null!;
}