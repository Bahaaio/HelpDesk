using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

public interface ICommentsService
{
    Task<List<CommentDto>> GetAll(int ticketId);
    Task<CommentDto> Create(int ticketId, CreateCommentRequest request);
}