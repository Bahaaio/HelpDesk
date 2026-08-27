using Workbench.Common.Exceptions;
using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Attachments.Models;
using Microsoft.EntityFrameworkCore;

namespace Workbench.Modules.Attachments.Repositories.Implementations;

/// <inheritdoc cref="IAttachmentsRepository{TAttachment}" />
public class AttachmentsRepository<TAttachment> : Repository<TAttachment, Guid>,
    IAttachmentsRepository<TAttachment>
    where TAttachment : Attachment, IHasParent, new()
{
    public AttachmentsRepository(AppDbContext context) : base(context)
    {
    }

    public Task<int> CountByParentIdAsync(int parentId) =>
        DbSet.CountAsync(a => a.ParentId == parentId);

    public Task<List<string>> GetIdsByParentIdAsync(int parentId) =>
        DbSet
            .Where(a => a.ParentId == parentId)
            .Select(a => a.Id.ToString())
            .ToListAsync();

    public async Task<int> GetParentIdByAttachmentAsync(Guid attachmentId) =>
        await DbSet
            .Where(a => a.Id == attachmentId)
            .Select(a => (int?)a.ParentId)
            .SingleOrDefaultAsync()
        ?? throw new NotFoundException($"Attachment with id: {attachmentId} not found");
}
