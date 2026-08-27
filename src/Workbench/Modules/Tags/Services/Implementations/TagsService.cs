using Workbench.Common.Exceptions;
using Workbench.Data.Persistence;
using Workbench.Modules.Tags.Dtos;
using Workbench.Modules.Tags.Dtos.Requests;
using Workbench.Modules.Tags.Mappers;
using Workbench.Modules.Tags.Models;
using Workbench.Modules.Tags.Repositories;

namespace Workbench.Modules.Tags.Services.Implementations;

public class TagsService : ITagsService
{
    private readonly ILogger<TagsService> _logger;
    private readonly ITagsRepository _tagsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TagsService(ITagsRepository tagsRepository, IUnitOfWork unitOfWork,
        ILogger<TagsService> logger)
    {
        _tagsRepository = tagsRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public Task<List<TagDto>> GetAll() => _tagsRepository.GetAllAsync();

    public async Task<TagDto> Create(CreateTagRequest request)
    {
        var existingTag = await _tagsRepository.FindByNameAsync(request.Name);
        if (existingTag is not null)
            throw new ConflictException($"Tag with name {request.Name} already exists");

        var tag = new Tag
        {
            Name = request.Name.ToLower(),
            Description = request.Description
        };

        _tagsRepository.Add(tag);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Created tag {tagName}", tag.Name);

        return tag.ToDto();
    }

    public async Task<TagDto> Update(string name, UpdateTagRequest request)
    {
        var tag = await _tagsRepository.FindByNameAsync(name)
                  ?? throw new NotFoundException($"Tag with name {name} doesn't exist");

        tag.Description = request.Description;

        await _unitOfWork.SaveChangesAsync();
        return tag.ToDto();
    }

    public async Task Delete(string name)
    {
        var deleted = await _tagsRepository.DeleteByNameAsync(name);

        if (deleted > 0)
            _logger.LogInformation("Deleted tag {tagName}", name);
    }
}
