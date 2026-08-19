using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Extensions;
using HelpDesk.Api.Models;
using HelpDesk.Api.Options;
using HelpDesk.Api.Services.Attachments;
using HelpDesk.Api.Services.Auth;
using HelpDesk.Api.Services.Storage;
using Microsoft.Extensions.Options;

namespace HelpDesk.Api.Services.Tickets;

public class TicketAttachmentsService : AttachmentsService<Ticket, TicketAttachment>
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly AppDbContext _db;

    public TicketAttachmentsService(IStorageService storageService, AppDbContext db,
        ICurrentUser user, ILogger<TicketAttachmentsService> logger,
        IAttachmentValidationService attachmentValidationService,
        IAuthorizationGuard authGuard, IOptions<TicketAttachmentOptions> attachmentOptions)
        : base(storageService, db, user, logger, attachmentValidationService)
    {
        _db = db;
        _authGuard = authGuard;
        AttachmentOptions = attachmentOptions.Value;
    }

    protected override AttachmentOptions AttachmentOptions { get; }

    public override async Task<AttachmentDto> Add(int ownerId, IFormFile file)
    {
        var ticket = await _db.Tickets.FindOrThrowAsync(ownerId);
        await _authGuard.AuthorizeOwnerOrTechnician(ticket);

        return await base.Add(ownerId, file);
    }

    public override async Task Delete(Guid attachmentId)
    {
        var ticket = await GetOwnerEntity(attachmentId);
        await _authGuard.AuthorizeOwnerOrTechnician(ticket);

        await base.Delete(attachmentId);
    }
}