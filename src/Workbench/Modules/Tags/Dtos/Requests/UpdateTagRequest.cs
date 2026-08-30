using System.ComponentModel.DataAnnotations;
using Workbench.Common.Enums;

namespace Workbench.Modules.Tags.Dtos.Requests;

public record UpdateTagRequest(
    [MaxLength(2000)] string? Description,
    [Required] Color Color
);