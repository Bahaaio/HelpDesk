namespace HelpDesk.Api.Services.Tickets;

/// <summary>
///     Manages the tags associated with a specific ticket.
/// </summary>
public interface ITicketTagsService
{
    /// <summary>
    ///     Replaces all tags on a ticket with the specified tag names. Requires technician role.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to update tags for.</param>
    /// <param name="tags">The list of tag names to assign to the ticket.</param>
    Task<List<string>> UpdateTags(int ticketId, List<string> tags);
}