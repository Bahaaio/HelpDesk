using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Extensions;
using HelpDesk.Api.Models;

namespace HelpDesk.Api.Services;

public class TicketAttachmentsService : AttachmentsService<TicketAttachment>
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly AppDbContext _db;

    public TicketAttachmentsService(IStorageService storageService, AppDbContext db,
        ICurrentUser user, ILogger<TicketAttachmentsService> logger,
        IAttachmentValidationService attachmentValidationService,
        IAuthorizationGuard authGuard) :
        base(storageService, db, user, logger, attachmentValidationService)
    {
        _db = db;
        _authGuard = authGuard;
    }

    public override async Task<AttachmentDto> Add(int ticketId, IFormFile file)
    {
        var ticket = await _db.Tickets.FindOrThrowAsync(ticketId);
        await _authGuard.AuthorizeOwnerOrTechnician(ticket);

        return await base.Add(ticketId, file);
    }

    public override async Task Delete(Guid attachmentId)
    {
        var attachment = await _db.Attachments.FindOrThrowAsync(attachmentId);
        var ticket = await _db.Tickets.FindOrThrowAsync(attachment.ResourceId);
        await _authGuard.AuthorizeOwnerOrTechnician(ticket);

        await base.Delete(attachmentId);
    }

    protected override Attachment CreateAttachment(int resourceId, IFormFile file, int uploaderId)
        => new TicketAttachment
        {
            TicketId = resourceId,
            ContentType = file.ContentType,
            OriginalFileName = file.FileName,
            UploaderId = uploaderId
        };
}