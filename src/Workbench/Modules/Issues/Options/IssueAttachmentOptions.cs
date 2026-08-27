using Workbench.Common.Options;
using Workbench.Modules.Attachments;
using Workbench.Modules.Attachments.Options;

namespace Workbench.Modules.Issues.Options;

public class IssueAttachmentOptions : AttachmentOptions, IKeyableOptions
{
    public static string Key => $"{BaseKey}:Issues";
}
