using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Auth.Dtos;

public record LoginRequest(
    [Required] [MinLength(2)] string Username,
    [Required] [MinLength(8)] string Password,
    bool RememberMe
);
