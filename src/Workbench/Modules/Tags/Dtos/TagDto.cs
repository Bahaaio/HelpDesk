using Workbench.Common.Enums;

namespace Workbench.Modules.Tags.Dtos;

public record TagDto(string Name, string? Description, Color Color);