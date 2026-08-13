using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttachmentsController(AttachmentsService attachmentsService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var stream = await attachmentsService.GetAttachment(id);
        return File(stream, "image/jpeg");
    }
}