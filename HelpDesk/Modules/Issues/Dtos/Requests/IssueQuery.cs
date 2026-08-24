using HelpDesk.Modules.Issues.Enums;

namespace HelpDesk.Modules.Issues.Dtos.Requests;

public record IssueQuery(
    Status? Status,
    string? Tag,
    string? Author,
    string? Q,
    IssueSort Sort = IssueSort.Latest
);