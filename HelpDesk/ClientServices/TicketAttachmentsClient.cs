using HelpDesk.Dtos.Responses;
using HelpDesk.Models;
using HelpDesk.Services.Attachments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;

namespace HelpDesk.ClientServices;

public class TicketAttachmentsClient : ITicketAttachmentsClient
{
    private const long MaximumFileSize = 10 * 1024 * 1024;
    private readonly IAttachmentsService<Ticket> _attachmentsService;

    public TicketAttachmentsClient(IAttachmentsService<Ticket> attachmentsService)
    {
        _attachmentsService = attachmentsService;
    }

    public async Task<List<AttachmentDto>> GetAll(int ticketId) =>
        await _attachmentsService.GetAll(ticketId);

    public async Task<AttachmentDto> Add(int ticketId, IBrowserFile file)
    {
        await using var source = file.OpenReadStream(MaximumFileSize);
        await using var stream = new MemoryStream();
        await source.CopyToAsync(stream);
        stream.Position = 0;

        var formFile = new FormFile(stream, 0, stream.Length, "file", file.Name)
        {
            Headers = new HeaderDictionary(),
            ContentType = file.ContentType ?? "application/octet-stream"
        };

        return await _attachmentsService.Add(ticketId, formFile);
    }

    public async Task Delete(int ticketId, Guid attachmentId)
    {
        await _attachmentsService.Delete(attachmentId);
    }
}