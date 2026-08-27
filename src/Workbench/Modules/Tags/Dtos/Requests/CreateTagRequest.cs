using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Tags.Dtos.Requests;

public record CreateTagRequest(
    [Required] [MaxLength(50)] string Name,
    [MaxLength(2000)] string? Description
);
