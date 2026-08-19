using System.Linq.Expressions;
using HelpDesk.Dtos.Responses;
using HelpDesk.Models;

namespace HelpDesk.Mappers;

public static class TagMapper
{
    private static readonly Func<Tag, TagDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Tag, TagDto>> ToDtoExpression =>
        t => new TagDto(t.Name, t.Description);

    public static TagDto ToDto(this Tag t) => Compiled(t);
}
