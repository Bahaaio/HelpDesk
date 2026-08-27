using Workbench.Common.Options;
using Workbench.Modules.Attachments;
using Workbench.Modules.Attachments.Options;

namespace Workbench.Modules.Comments.Options;

public class CommentAttachmentOptions : AttachmentOptions, IKeyableOptions
{
    public static string Key => $"{BaseKey}:Comments";
}
