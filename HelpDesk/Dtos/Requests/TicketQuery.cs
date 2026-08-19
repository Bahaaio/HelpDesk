using HelpDesk.Models.Enums;

namespace HelpDesk.Dtos.Requests;

public record TicketQuery(
    Status? Status,
    string? Tag,
    string? Author,
    string? Q,
    TicketSort Sort = TicketSort.Latest
);
