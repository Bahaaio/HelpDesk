using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Services.Comments;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/tickets/{ticketId:int}/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;

    public CommentsController(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CommentDto>>> GetAll(int ticketId) =>
        Ok(await _commentsService.GetAll(ticketId));

    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create(int ticketId, CreateCommentRequest request)
    {
        var comment = await _commentsService.Create(ticketId, request);
        return Created((string?)null, comment);
    }

    [HttpDelete("{commentId:int}")]
    public async Task<ActionResult> Delete(int ticketId, int commentId)
    {
        await _commentsService.Delete(commentId);
        return NoContent();
    }
}