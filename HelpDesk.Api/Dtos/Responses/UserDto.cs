namespace HelpDesk.Api.Dtos.Responses;

public record UserDto(
    string Username,
    string Email,
    string Role
);