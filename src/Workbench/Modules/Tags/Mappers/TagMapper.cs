using System.Linq.Expressions;
using Workbench.Modules.Tags.Dtos;
using Workbench.Modules.Tags.Models;

namespace Workbench.Modules.Tags.Mappers;

public static class TagMapper
{
    private static readonly Func<Tag, TagDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Tag, TagDto>> ToDtoExpression =>
        t => new TagDto(t.Name, t.Description);

    public static TagDto ToDto(this Tag t) => Compiled(t);
}
