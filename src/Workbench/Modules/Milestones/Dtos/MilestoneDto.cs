namespace Workbench.Modules.Milestones.Dtos;

public record MilestoneDto
{
    public required int Id { get; init; }
    public required int ProjectId { get; init; }
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required DateTime? DueDate { get; init; }
    public required int TotalItems { get; init; }
    public required int CompletedItems { get; init; }
}
