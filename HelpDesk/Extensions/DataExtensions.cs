using HelpDesk.Data;

namespace HelpDesk.Extensions;

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
            await SeedData.InitializeAsync(scope.ServiceProvider);
        }
    }
}