namespace Workbench.Common.Exceptions;

public static class DependencyInjection
{
    /// <summary>
    ///     Adds exception handling services.
    /// </summary>
    /// <param name="services"></param>
    public static void AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
    }
}
