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
