namespace HelpDesk.Services.Auth;

/// <summary>
///     Generates cryptographically secure random tokens.
/// </summary>
public interface ITokensService
{
    /// <summary>
    ///     Generates a URL-safe random token of the specified byte length.
    /// </summary>
    /// <param name="byteLength">Number of random bytes (output will be ~4/3 of this in chars).</param>
    string Generate(int byteLength);
}
