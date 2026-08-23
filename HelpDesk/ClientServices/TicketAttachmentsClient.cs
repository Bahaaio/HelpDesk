using HelpDesk.Dtos.Responses;
using HelpDesk.Models;
using HelpDesk.Options;
using HelpDesk.Services.Attachments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;

namespace HelpDesk.ClientServices;

public class TicketAttachmentsClient : ITicketAttachmentsClient
{
    private readonly IAttachmentsService<Ticket> _attachmentsService;
    private readonly TicketAttachmentOptions _options;

    public TicketAttachmentsClient(IAttachmentsService<Ticket> attachmentsService,
        IOptions<TicketAttachmentOptions> options)
    {
        _attachmentsService = attachmentsService;
        _options = options.Value;
    }

    public async Task<List<AttachmentDto>> GetAll(int ticketId) =>
        await _attachmentsService.GetAll(ticketId);

    public async Task<AttachmentDto> Add(int ticketId, IBrowserFile file)
    {
        await using var source = file.OpenReadStream(_options.MaxSizeBytes);
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