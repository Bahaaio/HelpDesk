using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Attachments.Services;

namespace Workbench.Modules.Attachments.Controllers;

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

        // Set caching headers to allow clients
        // to cache the attachment for 1 year and mark it as immutable
        Response.Headers.CacheControl = "public, max-age=31536000, immutable";

        return File(attachment.Stream, attachment.ContentType, attachment.OriginalFileName);
    }
}