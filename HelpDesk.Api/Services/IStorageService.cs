namespace HelpDesk.Api.Services;

public interface IStorageService
{
    Task Store(IFormFile file, string key);
    Task<Stream?> Load(string key);
    Task DeleteFile(string key);
}