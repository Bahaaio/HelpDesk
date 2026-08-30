using MudBlazor;
using Color = Workbench.Common.Enums.Color;

namespace Workbench.ClientServices;

public static class ColorMapper
{
    public static MudBlazor.Color ToMudColor(this Color color) => color switch
    {
        Color.Gray => MudBlazor.Color.Default,
        Color.Red => MudBlazor.Color.Error,
        Color.Green => MudBlazor.Color.Success,
        Color.Blue => MudBlazor.Color.Info,
        Color.Orange => MudBlazor.Color.Warning,
        Color.Purple => MudBlazor.Color.Secondary,
        _ => MudBlazor.Color.Default
    };
}
