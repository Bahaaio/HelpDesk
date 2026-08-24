using System.ComponentModel.DataAnnotations;
using HelpDesk.Common.Options;

namespace HelpDesk.Modules.Auth;

public class DefaultTechnicianOptions : IKeyableOptions
{
    /// <summary>
    ///     Default technician username.
    /// </summary>
    [Required]
    [MinLength(3)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    ///     Default technician email.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     Default technician password.
    /// </summary>
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    public static string Key => "DefaultTechnician";
}
