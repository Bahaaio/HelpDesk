using HelpDesk.Api.Data;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Services;

public class TagsService(AppDbContext db)
{
    public async Task<List<TagDto>> GetAll()
    {
        return await db.Tags
            .AsNoTracking()
            .Select(t => new TagDto(t.Name, t.Description))
            .ToListAsync();
    }

    public async Task<TagDto> Create(CreateTagRequest request)
    {
        var existingTag = await db.Tags.FirstOrDefaultAsync(t => t.Name == request.Name);
        if (existingTag is not null)
            throw new ConflictException($"Tag with name {request.Name} already exists");

        var tag = new Tag
        {
            Name = request.Name.ToLower(),
            Description = request.Description
        };

        await db.Tags.AddAsync(tag);
        await db.SaveChangesAsync();

        return new TagDto(tag.Name, tag.Description);
    }

    public async Task<TagDto> Update(string name, UpdateTagRequest request)
    {
        var tag = await db.Tags.FirstOrDefaultAsync(t => t.Name == name.ToLower());
        if (tag is null)
            throw new NotFoundException($"Tag with name {name} doesn't exist");

        tag.Description = request.Description;

        await db.SaveChangesAsync();
        return new TagDto(tag.Name, tag.Description);
    }
}