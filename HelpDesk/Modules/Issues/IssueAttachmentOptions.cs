using HelpDesk.Common.Options;
using HelpDesk.Modules.Attachments;

namespace HelpDesk.Modules.Issues;

public class IssueAttachmentOptions : AttachmentOptions, IKeyableOptions
{
    public static string Key => $"{BaseKey}:Issues";
}
