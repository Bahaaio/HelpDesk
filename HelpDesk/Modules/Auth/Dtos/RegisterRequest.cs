using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Modules.Auth.Dtos;

public record RegisterRequest(
    [Required] [MinLength(2)] string Username,
    [Required] [EmailAddress] string Email,
    [Required] [MinLength(8)] string Password,
    bool RememberMe,
    string? Code
);