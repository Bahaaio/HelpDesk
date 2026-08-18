using System.Security.Cryptography;

namespace HelpDesk.Api.Services.Auth;

public class TokensService : ITokensService
{
    public string Generate(int byteLength)
    {
        var bytes = RandomNumberGenerator.GetBytes(byteLength);
        return Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');
    }
}