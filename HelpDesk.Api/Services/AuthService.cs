using System.Security.Authentication;
using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Models;
using HelpDesk.Api.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Api.Services;

public class AuthService(
    UserManager<ApplicationUser> userManager,
    JwtTokenService jwtTokenService,
    RefreshTokenService refreshTokenService,
    AppDbContext db
)
{
    public async Task<AuthResult> Register(RegisterRequest registerRequest)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        var user = new ApplicationUser
        {
            UserName = registerRequest.Username,
            Email = registerRequest.Email
        };

        try
        {
            var result = await userManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded)
                throw new AuthenticationException("Invalid username or password" + result.Errors);

            await userManager.AddToRoleAsync(user, Role.Employee);

            var refreshToken = await refreshTokenService.CreateRefreshTokenForUser(user);
            var accessToken = await jwtTokenService.GenerateAccessToken(user);

            await transaction.CommitAsync();
            return new AuthResult(accessToken, refreshToken);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<AuthResult> Login(LoginRequest loginRequest)
    {
        var user = await userManager.FindByNameAsync(loginRequest.Username);

        if (user is null)
            throw new AuthenticationException("Invalid username or password");

        var valid = await userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (!valid)
            throw new AuthenticationException("Invalid username or password");

        var accessToken = await jwtTokenService.GenerateAccessToken(user);
        var refreshToken = await refreshTokenService.CreateRefreshTokenForUser(user);

        return new AuthResult(accessToken, refreshToken);
    }

    public async Task<AuthResult> Refresh(string refreshToken)
    {
        var token = await refreshTokenService.ValidateAndGetToken(refreshToken);

        var newRefreshToken = await refreshTokenService.RotateRefreshToken(token);
        var accessToken = await jwtTokenService.GenerateAccessToken(token.User);

        return new AuthResult(accessToken, newRefreshToken);
    }
}