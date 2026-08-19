using System.Linq.Expressions;
using HelpDesk.Dtos.Responses;
using HelpDesk.Models;

namespace HelpDesk.Mappers;

public static class CommentMapper
{
    private static readonly Func<Comment, CommentDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Comment, CommentDto>> ToDtoExpression => c =>
        new CommentDto(c.Id, c.Content, c.CreatedAt, c.Author.UserName!);

    public static CommentDto ToDto(this Comment c) => Compiled(c);
}
