using System.Linq.Expressions;
using HelpDesk.Modules.Tags.Dtos;
using HelpDesk.Modules.Tags.Models;

namespace HelpDesk.Modules.Tags.Mappers;

public static class TagMapper
{
    private static readonly Func<Tag, TagDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Tag, TagDto>> ToDtoExpression =>
        t => new TagDto(t.Name, t.Description);

    public static TagDto ToDto(this Tag t) => Compiled(t);
}