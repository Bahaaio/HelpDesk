using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments.Services;

namespace HelpDesk.Modules.Comments.Dtos;

public record CommentDto(
    int Id,
    string Content,
    DateTime CreatedAt,
    string AuthorUsername,
    List<AttachmentDto> Attachments
);