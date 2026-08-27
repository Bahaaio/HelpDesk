using Workbench.Modules.Attachments.Models;

namespace Workbench.Modules.Attachments.Repositories;

public interface IAttachmentsReadRepository
{
    Task<Attachment> GetByIdAsync(Guid attachmentId);
}
