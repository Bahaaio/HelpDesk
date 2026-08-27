using System.Linq.Expressions;

using Workbench.Modules.Attachments.Dtos;
using Workbench.Modules.Attachments.Models;

namespace Workbench.Modules.Attachments.Mappers;

public static class AttachmentMapper
{
    private static readonly Func<Attachment, AttachmentDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Attachment, AttachmentDto>> ToDtoExpression =>
        a => new AttachmentDto(a.Id, a.ContentType, a.OriginalFileName);

    public static AttachmentDto ToDto(this Attachment a) => Compiled(a);
}
