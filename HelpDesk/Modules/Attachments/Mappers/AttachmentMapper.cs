using System.Linq.Expressions;

using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;

namespace HelpDesk.Modules.Attachments.Mappers;

public static class AttachmentMapper
{
    private static readonly Func<Attachment, AttachmentDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Attachment, AttachmentDto>> ToDtoExpression =>
        a => new AttachmentDto(a.Id, a.ContentType, a.OriginalFileName);

    public static AttachmentDto ToDto(this Attachment a) => Compiled(a);
}