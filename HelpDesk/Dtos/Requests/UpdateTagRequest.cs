using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Dtos.Requests;

public record UpdateTagRequest([MaxLength(2000)] string? Description);
