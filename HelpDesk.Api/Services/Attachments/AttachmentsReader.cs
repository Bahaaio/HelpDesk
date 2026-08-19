using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Services.Storage;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services.Attachments;

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