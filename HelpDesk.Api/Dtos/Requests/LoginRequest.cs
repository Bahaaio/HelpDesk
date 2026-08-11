namespace HelpDesk.Api.Dtos.Requests;

public record LoginRequest(
    string Username,
    string Password
);