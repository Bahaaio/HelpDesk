namespace HelpDesk.Api.Services;

/// <summary>
///     Provides file storage operations (store, load, delete).
/// </summary>
public interface IStorageService
{
    /// <summary>
    ///     Stores a file with the given key.
    /// </summary>
    /// <param name="file">The file to store.</param>
    /// <param name="key">The unique key to identify the stored file.</param>
    Task Store(IFormFile file, string key);

    /// <summary>
    ///     Loads a file stream by key or null if not found.
    /// </summary>
    /// <param name="key">The unique key of the file to load.</param>
    Task<Stream?> Load(string key);

    /// <summary>
    ///     Deletes a file by key.
    /// </summary>
    /// <param name="key">The unique key of the file to delete.</param>
    Task DeleteFile(string key);
}