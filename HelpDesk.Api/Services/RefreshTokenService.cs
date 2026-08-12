using System.Security.Cryptography;
using System.Text;
using HelpDesk.Api.Data;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class RefreshTokenService(AppDbContext db)
{
    private const int Size = 64;
    private const int ExpirationDays = 7;

    public async Task<string> CreateRefreshTokenForUser(ApplicationUser user)
    {
        var token = GenerateRefreshToken();
        var hash = HashToken(token);
        var expiresAt = DateTime.UtcNow.AddDays(ExpirationDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Hash = hash,
            ExpiresAt = expiresAt,
            UserId = user.Id
        });

        await db.SaveChangesAsync();
        return token;
    }

    public async Task<string> RotateRefreshToken(RefreshToken oldToken)
    {
        oldToken.IsRevoked = true;
        return await CreateRefreshTokenForUser(oldToken.User);
    }

    public async Task RevokeRefreshToken(string refreshToken)
    {
        var hash = HashToken(refreshToken);
        var token = await db.RefreshTokens.SingleOrDefaultAsync(r => r.Hash == hash);

        if (token is not null)
        {
            token.IsRevoked = true;
            await db.SaveChangesAsync();
        }
    }

    public async Task<RefreshToken> ValidateAndGetToken(string refreshToken)
    {
        var hash = HashToken(refreshToken);

        var token = await db.RefreshTokens
            .Where(r => r.Hash == hash && !r.IsRevoked)
            .Include(r => r.User)
            .SingleOrDefaultAsync();

        if (token is null)
            throw new UnauthorizedException("Invalid refresh token");

        if (token.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedException("Refresh token expired");

        return token;
    }

    private static string HashToken(string token)
    {
        var bytes = Encoding.UTF8.GetBytes(token);
        var hash = SHA256.HashData(bytes);

        return Convert.ToBase64String(hash);
    }

    private static string GenerateRefreshToken()
    {
        var randomBytes = new byte[Size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}