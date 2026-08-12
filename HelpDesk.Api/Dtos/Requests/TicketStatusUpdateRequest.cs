using HelpDesk.Api.Models.Enums;

namespace HelpDesk.Api.Dtos.Requests;

public record TicketStatusUpdateRequest(Status Status);