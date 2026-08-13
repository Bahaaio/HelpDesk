using System.Security.Claims;
using HelpDesk.Api.Authorization.Requirements;
using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Api.Services;

public class AttachmentsService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly AppDbContext _db;
    private readonly StorageService _storageService;

    public AttachmentsService(StorageService storageService, AppDbContext db,
        IAuthorizationService authorizationService)
    {
        _storageService = storageService;
        _db = db;
        _authorizationService = authorizationService;
    }

    public async Task<AttachmentResponse> AddAttachment(int ticketId, IFormFile file,
        ClaimsPrincipal user)
    {
        var ticket = await _db.Tickets.FindAsync(ticketId);

        if (ticket is null)
            throw new NotFoundException($"Ticket with id {ticketId} not found");

        var result = await _authorizationService.AuthorizeAsync(
            user,
            ticket,
            new TicketOwnerOrTechnicianRequirement()
        );

        if (!result.Succeeded)
            throw new ForbiddenException(
                "You are not authorized to add an attachment to this ticket");

        var guid = Guid.NewGuid();
        var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _storageService.Store(file, guid.ToString());

        var attachment = new Attachment
        {
            Id = guid,
            TicketId = ticketId,
            UploaderId = userId
        };

        await _db.Attachments.AddAsync(attachment);
        await _db.SaveChangesAsync();

        return new AttachmentResponse(attachment.Id);
    }

    public async Task DeleteAttachment(Guid attachmentId, ClaimsPrincipal user)
    {
        var attachment = await _db.Attachments.FindAsync(attachmentId);
        if (attachment is null)
            throw new NotFoundException($"Attachment with id: {attachmentId} not found");

        var result = await _authorizationService.AuthorizeAsync(
            user,
            attachment,
            new AttachmentUploaderOrTechnicianRequirement()
        );

        if (!result.Succeeded)
            throw new ForbiddenException("You are not authorized to delete this attachment");

        _db.Remove(attachment);
        await _storageService.DeleteFile(attachmentId.ToString());

        await _db.SaveChangesAsync();
    }

    public async Task<Stream> GetAttachment(Guid attachmentId)
    {
        var stream = await _storageService.Load(attachmentId.ToString());

        if (stream is null)
            throw new NotFoundException($"Attachment with id: {attachmentId} not found");

        return stream;
    }
}