using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Services.Comments;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

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

    [HttpPut("{commentId:int}")]
    public async Task<ActionResult<CommentDto>> Update(int ticketId, int commentId, UpdateCommentRequest request) =>
        Ok(await _commentsService.Update(commentId, request));

    [HttpDelete("{commentId:int}")]
    public async Task<ActionResult> Delete(int ticketId, int commentId)
    {
        await _commentsService.Delete(commentId);
        return NoContent();
    }
}
