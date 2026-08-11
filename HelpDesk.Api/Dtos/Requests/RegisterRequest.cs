using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Dtos.Requests;

public record RegisterRequest(
    [MinLength(2)] string Username,
    [EmailAddress] string Email,
    [MinLength(8)] string Password
    // TODO: full name?
);