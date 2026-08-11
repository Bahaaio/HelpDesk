namespace HelpDesk.Api.Dtos.Responses;

public record AuthResult(
    string AccessToken,
    string RefreshToken
);