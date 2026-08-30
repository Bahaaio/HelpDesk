namespace Workbench.Modules.Kanban.Dtos;

public record BoardDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required List<ColumnDto> Columns { get; init; }
}
