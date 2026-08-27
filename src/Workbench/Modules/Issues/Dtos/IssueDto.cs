using Workbench.Modules.Attachments.Dtos;
using Workbench.Modules.Attachments.Models;
using Workbench.Modules.Attachments.Services;
using Workbench.Modules.Issues.Enums;

namespace Workbench.Modules.Issues.Dtos;

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
    public required List<AttachmentDto> Attachments { get; init; }
    public required int VoteScore { get; init; }
}
