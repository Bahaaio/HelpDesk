using Workbench.Modules.Attachments.Dtos;
using Workbench.Modules.Attachments.Models;
using Workbench.Modules.Attachments.Services;

namespace Workbench.Modules.Comments.Dtos;

public record CommentDto(
    int Id,
    string Content,
    DateTime CreatedAt,
    string AuthorUsername,
    List<AttachmentDto> Attachments
);
