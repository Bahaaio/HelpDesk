using HelpDesk.Modules.Auth.Services.Implementations;

namespace HelpDesk.Common.Extensions;

public static class DataExtensions
{
    extension(WebApplication app)
    {
        /// <summary>
        ///     Seeds the database with initial data.
        /// </summary>
        public async Task SeedDataAsync()
        {
            using var scope = app.Services.CreateScope();
            await AuthSeeder.InitializeAsync(scope.ServiceProvider);
        }
    }
}