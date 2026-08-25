using HelpDesk.Modules.Storage.Services;
using HelpDesk.Modules.Storage.Services.Implementations;

namespace HelpDesk.Modules.Storage;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddStorageModule()
        {
            services.AddScoped<IStorageService, LocalStorageService>();
        }
    }
}