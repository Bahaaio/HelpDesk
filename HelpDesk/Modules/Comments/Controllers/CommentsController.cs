using HelpDesk.Modules.Comments.Dtos;
using HelpDesk.Modules.Comments.Dtos.Requests;
using HelpDesk.Modules.Comments.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Modules.Comments.Controllers;

[ApiController]
[Route("api/issues/{issueId:int}/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;

    public CommentsController(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CommentDto>>> GetAll(int issueId) =>
        Ok(await _commentsService.GetAll(issueId));

    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create(int issueId, CreateCommentRequest request)
    {
        var comment = await _commentsService.Create(issueId, request);
        return Created((string?)null, comment);
    }

    [HttpPut("{commentId:int}")]
    public async Task<ActionResult<CommentDto>> Update(int issueId, int commentId,
        UpdateCommentRequest request) =>
        Ok(await _commentsService.Update(commentId, request));

    [HttpDelete("{commentId:int}")]
    public async Task<ActionResult> Delete(int issueId, int commentId)
    {
        await _commentsService.Delete(commentId);
        return NoContent();
    }
}