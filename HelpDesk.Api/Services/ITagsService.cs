using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

public interface ITagsService
{
    Task<List<TagDto>> GetAll();
    Task<TagDto> Create(CreateTagRequest request);
    Task<TagDto> Update(string name, UpdateTagRequest request);
}