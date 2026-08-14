using HelpDesk.Api.Models.Enums;

namespace HelpDesk.Api.Dtos.Requests;

public record UpdateTicketStatusRequest(Status Status);