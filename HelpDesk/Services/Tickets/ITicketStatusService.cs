using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;

namespace HelpDesk.Services.Tickets;

/// <summary>
///     Manages ticket status changes and history.
/// </summary>
public interface ITicketStatusService
{
    /// <summary>
    ///     Updates the status of a ticket. Only the ticket author or a technician may change status.
    /// </summary>
    /// <param name="ticketId">The ticket ID.</param>
    /// <param name="request">The new status value.</param>
    Task UpdateStatus(int ticketId, UpdateTicketStatusRequest request);

    /// <summary>
    ///     Returns the status change history for a ticket.
    /// </summary>
    /// <param name="ticketId">The ticket ID.</param>
    Task<List<StatusChangeDto>> GetStatusHistory(int ticketId);
}
