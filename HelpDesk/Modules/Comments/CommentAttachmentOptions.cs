using HelpDesk.Common.Options;
using HelpDesk.Modules.Attachments;

namespace HelpDesk.Modules.Comments;

public class CommentAttachmentOptions : AttachmentOptions, IKeyableOptions
{
    public static string Key => $"{BaseKey}:Comments";
}
