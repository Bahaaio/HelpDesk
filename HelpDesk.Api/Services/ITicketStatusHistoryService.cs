using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

/// <summary>
///     Service interface for managing ticket status history.
/// </summary>
public interface ITicketStatusHistoryService
{
    /// <summary>
    ///     Get the status history of a ticket by its ID.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket.</param>
    /// <returns>A list of status change DTOs representing the status history of the ticket.</returns>
    Task<List<StatusChangeDto>> GetStatusHistory(int ticketId);
}