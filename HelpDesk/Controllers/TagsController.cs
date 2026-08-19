using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Models.Enums;
using HelpDesk.Services.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ITagsService _tagsService;

    public TagsController(ITagsService tagsService)
    {
        _tagsService = tagsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TagDto>>> GetAll() => Ok(await _tagsService.GetAll());

    [Authorize(Roles = Role.Technician)]
    [HttpPost]
    public async Task<ActionResult<TagDto>> Create(CreateTagRequest request)
    {
        var tag = await _tagsService.Create(request);
        return Created((string?)null, tag);
    }

    [Authorize(Roles = Role.Technician)]
    [HttpPut("{name}")]
    public async Task<ActionResult<TagDto>> Update(string name, UpdateTagRequest request) =>
        Ok(await _tagsService.Update(name, request));

    [Authorize(Roles = Role.Technician)]
    [HttpDelete("{name}")]
    public async Task<ActionResult> Delete(string name)
    {
        await _tagsService.Delete(name);
        return NoContent();
    }
}
