using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Tags.Dtos;
using Workbench.Modules.Tags.Dtos.Requests;
using Workbench.Modules.Tags.Services;

namespace Workbench.Modules.Tags.Controllers;

[ApiController]
[Route("api/projects/{projectId:int}/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ITagsService _tagsService;

    public TagsController(ITagsService tagsService)
    {
        _tagsService = tagsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TagDto>>> GetAll(int projectId) =>
        Ok(await _tagsService.GetAll(projectId));

    [HttpPost]
    public async Task<ActionResult<TagDto>> Create(int projectId, CreateTagRequest request)
    {
        var tag = await _tagsService.Create(projectId, request);
        return Created((string?)null, tag);
    }

    [HttpPut("{tagName}")]
    public async Task<ActionResult<TagDto>> Update(int projectId, string tagName,
        UpdateTagRequest request) =>
        Ok(await _tagsService.Update(projectId, tagName, request));

    [HttpDelete("{tagName}")]
    public async Task<ActionResult> Delete(int projectId, string tagName)
    {
        await _tagsService.Delete(projectId, tagName);
        return NoContent();
    }
}