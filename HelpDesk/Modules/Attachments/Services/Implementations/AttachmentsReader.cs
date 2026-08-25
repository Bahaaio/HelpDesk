using HelpDesk.Common.Exceptions;
using HelpDesk.Data;
using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Storage.Services;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Attachments.Services.Implementations;

public class AttachmentsReader : IAttachmentsReader
{
    private readonly AppDbContext _db;
    private readonly IStorageService _storageService;

    public AttachmentsReader(AppDbContext db, IStorageService storageService)
    {
        _db = db;
        _storageService = storageService;
    }

    public async Task<AttachmentResult> Get(Guid attachmentId)
    {
        var stream = await _storageService.Load(attachmentId.ToString());

        var attachment = await _db.Attachments
            .Where(a => a.Id == attachmentId)
            .Select(a => new { a.ContentType, a.OriginalFileName })
            .SingleOrDefaultAsync();

        if (stream is null || attachment is null)
            throw new NotFoundException($"Attachment with id: {attachmentId} not found");

        return new AttachmentResult(stream, attachment.ContentType, attachment.OriginalFileName);
    }
}