namespace HelpDesk.Options;

public class IssueAttachmentOptions : AttachmentOptions, IKeyableOptions
{
    public static string Key => $"{BaseKey}:Issues";
}
