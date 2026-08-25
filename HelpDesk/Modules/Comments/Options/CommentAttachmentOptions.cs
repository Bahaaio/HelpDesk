using HelpDesk.Common.Options;
using HelpDesk.Modules.Attachments;
using HelpDesk.Modules.Attachments.Options;

namespace HelpDesk.Modules.Comments.Options;

public class CommentAttachmentOptions : AttachmentOptions, IKeyableOptions
{
    public static string Key => $"{BaseKey}:Comments";
}
