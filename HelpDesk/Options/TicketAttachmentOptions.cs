namespace HelpDesk.Options;

public class TicketAttachmentOptions : AttachmentOptions, IKeyableOptions
{
    public static string Key => $"{BaseKey}:Tickets";
}
