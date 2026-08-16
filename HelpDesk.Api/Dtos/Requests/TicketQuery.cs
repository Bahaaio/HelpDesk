using HelpDesk.Api.Models.Enums;

namespace HelpDesk.Api.Dtos.Requests;

public record TicketQuery(
    Status? Status,
    string? Tag,
    string? Author,
    string? Q,
    TicketSort Sort = TicketSort.Latest
);