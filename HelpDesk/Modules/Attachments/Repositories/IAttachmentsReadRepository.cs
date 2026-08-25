using HelpDesk.Modules.Attachments.Models;

namespace HelpDesk.Modules.Attachments.Repositories;

public interface IAttachmentsReadRepository
{
    Task<Attachment> GetByIdAsync(Guid attachmentId);
}