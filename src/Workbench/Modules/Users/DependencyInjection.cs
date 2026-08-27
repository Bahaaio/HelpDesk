using Workbench.Modules.Users.Services;
using Workbench.Modules.Users.Services.Implementations;

namespace Workbench.Modules.Users;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddUsersModule()
        {
            services.AddScoped<IUsersService, UsersService>();
        }
    }
}
