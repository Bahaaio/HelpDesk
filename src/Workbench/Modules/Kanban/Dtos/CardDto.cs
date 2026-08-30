using Workbench.Modules.Issues.Enums;

namespace Workbench.Modules.Kanban.Dtos;

public record CardDto
{
    public required int Id { get; init; }
    public required int Position { get; init; }
    public required int IssueId { get; init; }
    public required string IssueTitle { get; init; }
    public required Status IssueStatus { get; init; }
}