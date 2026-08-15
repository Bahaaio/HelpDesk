using HelpDesk.Api.Exceptions;

namespace HelpDesk.Api.Services;

/// <summary>
///     Validates uploaded attachments according to configuration and business rules.
/// </summary>
public interface IAttachmentValidationService
{
    /// <summary>
    ///     Validates an uploaded attachment file (size, extension, etc).
    /// </summary>
    /// <param name="file">The file to validate.</param>
    /// <exception cref="BadRequestException">Thrown on validation failure</exception>
    void Validate(IFormFile file);

    /// <summary>
    ///     Validates the number of attachments allowed per context (e.g., ticket).
    /// </summary>
    /// <param name="count">The current number of attachments.</param>
    /// <exception cref="BadRequestException">Thrown on validation failure</exception>
    void ValidateCount(int count);
}