using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Dtos.Requests;

public record LoginRequest(
    [Required] string Username,
    [Required] string Password
);