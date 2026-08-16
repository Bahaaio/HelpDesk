using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

public interface ITicketAssignmentsService
{
    /// <summary>
    ///     Assigns the current user to the ticket.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to assign.</param>
    Task AssignCurrentUser(int ticketId);

    /// <summary>
    ///     Unassigns the current user from the ticket.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to unassign.</param>
    Task UnassignCurrentUser(int ticketId);

    /// <summary>
    ///     Returns all tickets assigned to the current user.
    /// </summary>
    /// <param name="ticketQuery">Optional filters for status, tag, author, and free-text search.</param>
    Task<List<TicketDto>> GetCurrentUserAssignedTickets(TicketQuery ticketQuery);
}