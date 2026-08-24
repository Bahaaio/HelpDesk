using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Modules.Tags;

public record UpdateTagRequest([MaxLength(2000)] string? Description);