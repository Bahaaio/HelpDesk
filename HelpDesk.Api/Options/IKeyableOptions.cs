namespace HelpDesk.Api.Options;

/// <summary>
///     Options that can be retrieved from the configuration by a key.
/// </summary>
public interface IKeyableOptions
{
    /// <summary>
    ///     The key used to retrieve the options from the configuration.
    /// </summary>
    static abstract string Key { get; }
}