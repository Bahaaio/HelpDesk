using HelpDesk.Models.Enums;

namespace HelpDesk.Dtos.Responses;

public record IssueDto
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required Status Status { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string AuthorUsername { get; init; }
    public required string? AssignedToUsername { get; init; }
    public required List<string> Tags { get; init; }
    public required int VoteScore { get; init; }
}
