using Scalar.AspNetCore;

namespace HelpDesk.Api.Extensions;

public static class OpenApiExtensions
{
    public static void AddOpenApiServices(this IServiceCollection services)
    {
        services.AddOpenApi();
    }

    public static void UseOpenApiUi(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference("/docs");
    }
}