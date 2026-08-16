using HelpDesk.Api.Models.Enums;

namespace HelpDesk.Api.Dtos.Responses;

public record StatusChangeDto
{
    public required Status FromStatus { get; init; }
    public required Status ToStatus { get; init; }
    public required string ChangedByUsername { get; init; }
    public required DateTime ChangedAt { get; init; }
}
