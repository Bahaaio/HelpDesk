using System.Linq.Expressions;
using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Comments.Dtos;
using HelpDesk.Modules.Comments.Models;

using HelpDesk.Modules.Attachments.Mappers;

namespace HelpDesk.Modules.Comments.Mappers;

public static class CommentMapper
{
    private static readonly Func<Comment, CommentDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Comment, CommentDto>> ToDtoExpression => c =>
        new CommentDto(
            c.Id,
            c.Content,
            c.CreatedAt,
            c.Author.UserName!,
            c.Attachments.Select(ca => ca.ToDto()).ToList()
        );

    public static CommentDto ToDto(this Comment c) => Compiled(c);
}