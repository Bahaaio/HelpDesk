using HelpDesk.Common.Exceptions;
using HelpDesk.Data;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Tags;

public class TagsService : ITagsService
{
    private readonly AppDbContext _db;
    private readonly ILogger<TagsService> _logger;

    public TagsService(AppDbContext db, ILogger<TagsService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<TagDto>> GetAll() =>
        await _db.Tags
            .AsNoTracking()
            .Select(TagMapper.ToDtoExpression)
            .ToListAsync();

    public async Task<TagDto> Create(CreateTagRequest request)
    {
        var existingTag = await _db.Tags.SingleOrDefaultAsync(t => t.Name == request.Name);
        if (existingTag is not null)
            throw new ConflictException($"Tag with name {request.Name} already exists");

        var tag = new Tag
        {
            Name = request.Name.ToLower(),
            Description = request.Description
        };

        _db.Tags.Add(tag);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Created tag {tagName}", tag.Name);

        return tag.ToDto();
    }

    public async Task<TagDto> Update(string name, UpdateTagRequest request)
    {
        var tag = await _db.Tags.SingleOrDefaultAsync(t =>
            EF.Functions.ILike(t.Name, name));

        if (tag is null)
            throw new NotFoundException($"Tag with name {name} doesn't exist");

        tag.Description = request.Description;

        await _db.SaveChangesAsync();
        return tag.ToDto();
    }

    public async Task Delete(string name)
    {
        var deleted = await _db.Tags
            .Where(t => EF.Functions.ILike(t.Name, name))
            .ExecuteDeleteAsync();

        if (deleted > 0)
            _logger.LogInformation("Deleted tag {tagName}", name);
    }
}