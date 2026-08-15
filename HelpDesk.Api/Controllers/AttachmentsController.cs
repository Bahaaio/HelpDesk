using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttachmentsController : ControllerBase
{
    private readonly IAttachmentsService _attachmentsService;

    public AttachmentsController(IAttachmentsService attachmentsService)
    {
        _attachmentsService = attachmentsService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var result = await _attachmentsService.GetAttachment(id);
        return File(result.Stream, result.ContentType, result.OriginalFileName);
    }
}