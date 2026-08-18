using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Options;

namespace HelpDesk.Api.Services.Attachments;

/// <summary>
///     Validates uploaded attachments according to configuration and business rules.
/// </summary>
public interface IAttachmentValidationService
{
    /// <summary>
    ///     Validates an uploaded attachment file (size, extension, etc).
    /// </summary>
    /// <param name="file">The file to validate.</param>
    /// <param name="options">The attachment options to use for validation.</param>
    /// <exception cref="BadRequestException">Thrown on validation failure</exception>
    void Validate(IFormFile file, AttachmentOptions options);

    /// <summary>
    ///     Validates the number of attachments allowed per context (e.g., ticket).
    /// </summary>
    /// <param name="count">The current number of attachments.</param>
    /// <param name="maxCount">The maximum number of attachments allowed.</param>
    /// <exception cref="BadRequestException">Thrown on validation failure</exception>
    void ValidateCount(int count, int maxCount);
}