using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Options;

namespace HelpDesk.Api.Services;

public class AttachmentValidationService : IAttachmentValidationService
{
    public void Validate(IFormFile file, AttachmentOptions options)
    {
        ValidateSize(file, options);
        ValidateExtension(file, options);
    }

    public void ValidateCount(int count, int maxCount)
    {
        if (count > maxCount)
            throw new BadRequestException($"Maximum number of attachments exceeded ({maxCount})");
    }

    private void ValidateExtension(IFormFile file, AttachmentOptions options)
    {
        var extension = Path.GetExtension(file.FileName);

        if (!options.AllowedExtensions.Contains(extension))
            throw new BadRequestException("File extension is not allowed");
    }

    private void ValidateSize(IFormFile file, AttachmentOptions options)
    {
        if (file.Length == 0)
            throw new BadRequestException("File cannot be empty");

        if (file.Length > options.MaxSizeBytes)
            throw new BadRequestException($"File cannot exceed {options.MaxSizeBytes} bytes");
    }
}