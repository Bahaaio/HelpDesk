using HelpDesk.Api.Options;

namespace HelpDesk.Api.Extensions;

public static class OptionsExtensions
{
    public static void AddOptionsServices(this IServiceCollection services)
    {
        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.Key)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}