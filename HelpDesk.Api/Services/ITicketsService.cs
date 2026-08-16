using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;

namespace HelpDesk.Api.Services;

/// <summary>
///     Manages IT helpdesk tickets including CRUD, status changes, and filtering.
/// </summary>
public interface ITicketsService
{
    /// <summary>
    ///     Returns all tickets matching the specified filters.
    /// </summary>
    /// <param name="ticketQuery">Optional filters for status, tag, author, and free-text search.</param>
    Task<List<TicketDto>> GetAll(TicketQuery ticketQuery);

    /// <summary>
    ///     Returns tickets created by the current user, matching the specified filters.
    /// </summary>
    /// <param name="ticketQuery">Optional filters for status, tag, author, and free-text search.</param>
    Task<List<TicketDto>> GetCurrentUserTickets(TicketQuery ticketQuery);

    /// <summary>
    ///     Returns a single ticket by its ID.
    /// </summary>
    /// <param name="id">The ticket ID.</param>
    /// <exception cref="NotFoundException">Thrown if the ticket does not exist.</exception>
    Task<TicketDto> GetById(int id);

    /// <summary>
    ///     Creates a new ticket assigned to the current user.
    /// </summary>
    /// <param name="request">The ticket title and optional description.</param>
    Task<TicketDto> Create(CreateTicketRequest request);

    /// <summary>
    ///     Updates an existing ticket. Only the ticket author or a technician may update.
    /// </summary>
    /// <param name="id">The ticket ID.</param>
    /// <param name="request">The updated title and optional description.</param>
    Task<TicketDto> Update(int id, UpdateTicketRequest request);

    /// <summary>
    ///     Deletes a ticket and its attachments from storage. Only the ticket author or a technician may delete.
    /// </summary>
    /// <param name="id">The ticket ID.</param>
    Task Delete(int id);
}