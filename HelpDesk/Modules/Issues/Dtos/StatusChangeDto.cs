using HelpDesk.Modules.Issues.Enums;

namespace HelpDesk.Modules.Issues.Dtos;

public record StatusChangeDto
{
    public required Status FromStatus { get; init; }
    public required Status ToStatus { get; init; }
    public required string ChangedByUsername { get; init; }
    public required DateTime ChangedAt { get; init; }
}