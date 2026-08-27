using Workbench.Common.Extensions;
using Workbench.Data;
using Workbench.Modules.Attachments.Models;
using Microsoft.EntityFrameworkCore;

namespace Workbench.Modules.Attachments.Repositories.Implementations;

public class AttachmentsReadRepository : IAttachmentsReadRepository
{
    private readonly DbSet<Attachment> _attachments;

    public AttachmentsReadRepository(AppDbContext context)
    {
        _attachments = context.Set<Attachment>();
    }

    public Task<Attachment> GetByIdAsync(Guid attachmentId) =>
        _attachments.FindOrThrowAsync(attachmentId);
}
