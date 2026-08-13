namespace HelpDesk.Api.Services;

public interface ITicketTagsService
{
    Task<List<string>> UpdateTags(int ticketId, List<string> tags);
}