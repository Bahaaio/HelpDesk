using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Dtos.Requests;

public record LoginRequest(
    [Required] [MinLength(2)] string Username,
    [Required] [MinLength(8)] string Password,
    bool RememberMe
);
