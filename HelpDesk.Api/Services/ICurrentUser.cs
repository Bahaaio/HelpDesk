using System.Security.Claims;

namespace HelpDesk.Api.Services;

/// <summary>
///     Provides access to the current authenticated user's identity and claims.
/// </summary>
public interface ICurrentUser
{
    /// <summary>The user's database ID.</summary>
    public int Id { get; }

    /// <summary>The user's username.</summary>
    public string UserName { get; }

    /// <summary>The user's role (Employee or Technician).</summary>
    public string Role { get; }

    /// <summary>The underlying ClaimsPrincipal for authorization checks.</summary>
    public ClaimsPrincipal Principal { get; }
}