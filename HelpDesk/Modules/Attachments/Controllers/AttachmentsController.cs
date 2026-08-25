using HelpDesk.Modules.Attachments.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Modules.Attachments.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttachmentsController : ControllerBase
{
    private readonly IAttachmentsReader _attachmentsService;

    public AttachmentsController(IAttachmentsReader attachmentsService)
    {
        _attachmentsService = attachmentsService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var attachment = await _attachmentsService.Get(id);
        return File(attachment.Stream, attachment.ContentType, attachment.OriginalFileName);
    }
}