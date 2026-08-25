using HelpDesk.Modules.Users.Services;
using HelpDesk.Modules.Users.Services.Implementations;

namespace HelpDesk.Modules.Users;

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