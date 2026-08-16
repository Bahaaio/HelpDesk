using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Options;

public class DefaultTechnicianOptions
{
    public const string Key = "DefaultTechnician";

    [Required]
    [MinLength(3)]
    public string Username { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = default!;
}
