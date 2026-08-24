using System.Linq.Expressions;

namespace HelpDesk.Modules.Tags;

public static class TagMapper
{
    private static readonly Func<Tag, TagDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Tag, TagDto>> ToDtoExpression =>
        t => new TagDto(t.Name, t.Description);

    public static TagDto ToDto(this Tag t) => Compiled(t);
}