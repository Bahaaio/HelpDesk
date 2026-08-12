using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Dtos.Requests;

public record RegisterRequest(
    [Required] [MinLength(2)] string Username,
    [Required] [EmailAddress] string Email,
    [Required] [MinLength(8)] string Password,
    bool RememberMe
);