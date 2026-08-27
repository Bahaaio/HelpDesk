using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Tags.Dtos.Requests;

public record UpdateTagRequest([MaxLength(2000)] string? Description);
