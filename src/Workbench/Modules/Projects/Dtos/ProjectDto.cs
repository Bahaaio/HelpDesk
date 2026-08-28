namespace Workbench.Modules.Projects.Dtos;

public record ProjectDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string OwnerUsername { get; init; }
}