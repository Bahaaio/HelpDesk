using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/tickets/{ticketId:int}/[controller]")]
public class CommentsController(CommentsService commentsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CommentDto>>> GetAll(int ticketId)
    {
        return Ok(await commentsService.GetAll(ticketId));
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create(int ticketId, CreateCommentRequest request)
    {
        var comment = await commentsService.Create(ticketId, request, User);
        return Created((string?)null, comment);
    }
}