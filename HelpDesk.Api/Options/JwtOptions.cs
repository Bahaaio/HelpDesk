using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Options;

public class JwtOptions
{
    public const string Key = "Jwt";

    [Required] [MinLength(32)] public string SecretKey { get; set; } = string.Empty;
    [Required] public string Issuer { get; set; } = string.Empty;
    [Required] public string Audience { get; set; } = string.Empty;

    [Range(1, int.MaxValue)] public int ExpiresInMinutes { get; set; }
}