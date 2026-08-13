using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Models.Enums;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController(TagsService tagsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TagDto>>> GetAll()
    {
        return Ok(await tagsService.GetAll());
    }

    [Authorize(Roles = Role.Technician)]
    [HttpPost]
    public async Task<ActionResult<TagDto>> Create(CreateTagRequest request)
    {
        var tag = await tagsService.Create(request);
        return Created((string?)null, tag);
    }

    [Authorize(Roles = Role.Technician)]
    [HttpPut("{name}")]
    public async Task<ActionResult<TagDto>> Update(string name, UpdateTagRequest request)
    {
        return Ok(await tagsService.Update(name, request));
    }
}