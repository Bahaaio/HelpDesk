using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HelpDesk.Api.Models;
using HelpDesk.Api.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HelpDesk.Api.Services;

public class JwtTokenService(
    IOptions<JwtOptions> jwtOptions,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole<int>> roleManager
)
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<string> GenerateAccessToken(ApplicationUser user)
    {
        var key = Encoding.UTF8.GetBytes(_jwtOptions.SecretKey);
        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!)
        };

        var userClaims = await userManager.GetRolesAsync(user);
        var role = userClaims.FirstOrDefault();

        if (role is not null)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var expriation = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: expriation,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}