using HelpDesk.Models.Enums;

namespace HelpDesk.Dtos.Requests;

public record UpdateTicketStatusRequest(Status Status);
