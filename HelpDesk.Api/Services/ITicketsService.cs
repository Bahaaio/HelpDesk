using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

public interface ITicketsService
{
    Task<List<TicketDto>> GetAll(TicketQuery ticketQuery);
    Task<List<TicketDto>> GetCurrentUserTickets(TicketQuery ticketQuery);
    Task<TicketDto?> GetById(int id);
    Task<TicketDto> Create(CreateTicketRequest request);
    Task<TicketDto> Update(int id, UpdateTicketRequest request);
    Task UpdateStatus(int id, TicketStatusUpdateRequest request);
}