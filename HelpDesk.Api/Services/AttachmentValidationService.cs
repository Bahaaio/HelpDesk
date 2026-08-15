using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Options;
using Microsoft.Extensions.Options;

namespace HelpDesk.Api.Services;

public class AttachmentValidationService : IAttachmentValidationService
{
    private readonly AttachmentOptions _attachmentOptions;

    public AttachmentValidationService(IOptions<AttachmentOptions> attachmentOptions)
    {
        _attachmentOptions = attachmentOptions.Value;
    }

    public void Validate(IFormFile file)
    {
        ValidateSize(file);
        ValidateExtension(file);
    }

    public void ValidateCount(int count)
    {
        if (count > _attachmentOptions.MaxCount)
            throw new BadRequestException(
                $"Maximum number of attachments exceeded ({_attachmentOptions.MaxCount})");
    }

    private void ValidateExtension(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);

        if (!_attachmentOptions.AllowedExtensions.Contains(extension))
            throw new BadRequestException("File extension is not allowed");
    }

    private void ValidateSize(IFormFile file)
    {
        if (file.Length == 0)
            throw new BadRequestException("File cannot be empty");

        if (file.Length > _attachmentOptions.MaxSizeBytes)
            throw new BadRequestException(
                $"File cannot exceed {_attachmentOptions.MaxSizeBytes} bytes");
    }
}