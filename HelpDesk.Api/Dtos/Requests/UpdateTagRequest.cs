using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Dtos.Requests;

public record UpdateTagRequest([MaxLength(2000)] string? Description);