using HelpDesk.Common.Options;
using HelpDesk.Modules.Attachments;
using HelpDesk.Modules.Attachments.Options;

namespace HelpDesk.Modules.Issues.Options;

public class IssueAttachmentOptions : AttachmentOptions, IKeyableOptions
{
    public static string Key => $"{BaseKey}:Issues";
}
