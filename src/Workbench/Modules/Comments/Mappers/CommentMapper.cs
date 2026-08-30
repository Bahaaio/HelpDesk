using System.Linq.Expressions;
using Workbench.Modules.Attachments.Mappers;
using Workbench.Modules.Comments.Dtos;
using Workbench.Modules.Comments.Models;

namespace Workbench.Modules.Comments.Mappers;

public static class CommentMapper
{
    private static readonly Func<Comment, CommentDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Comment, CommentDto>> ToDtoExpression => c =>
        new CommentDto(
            c.Id,
            c.Content,
            c.CreatedAt,
            c.Author.UserName!,
            c.Attachments.AsQueryable().Select(AttachmentMapper.ToDtoExpression).ToList()
        );

    public static CommentDto ToDto(this Comment c) => Compiled(c);
}