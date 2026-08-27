using Workbench.Common.Exceptions;
using Workbench.Modules.Attachments.Dtos;
using Workbench.Modules.Attachments.Repositories;
using Workbench.Modules.Storage.Services;

namespace Workbench.Modules.Attachments.Services.Implementations;

public class AttachmentsReader : IAttachmentsReader
{
    private readonly IAttachmentsReadRepository _attachmentsReadRepository;
    private readonly IStorageService _storageService;

    public AttachmentsReader(IStorageService storageService,
        IAttachmentsReadRepository attachmentsReadRepository)
    {
        _storageService = storageService;
        _attachmentsReadRepository = attachmentsReadRepository;
    }

    public async Task<AttachmentResult> Get(Guid attachmentId)
    {
        var attachment = await _attachmentsReadRepository.GetByIdAsync(attachmentId);
        var stream = await _storageService.Load(attachmentId.ToString());

        if (stream is null)
            throw new NotFoundException($"Attachment with id: {attachmentId} not found");

        return new AttachmentResult(stream, attachment.ContentType, attachment.OriginalFileName);
    }
}
