using HelpDesk.Models.Enums;

namespace HelpDesk.Dtos.Requests;

public record IssueQuery(
    Status? Status,
    string? Tag,
    string? Author,
    string? Q,
    IssueSort Sort = IssueSort.Latest
);
