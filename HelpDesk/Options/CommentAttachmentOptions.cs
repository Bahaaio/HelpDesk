namespace HelpDesk.Options;

public class CommentAttachmentOptions : AttachmentOptions, IKeyableOptions
{
    public static string Key => $"{BaseKey}:Comments";
}