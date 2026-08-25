using HelpDesk.Common.Authorization;
using HelpDesk.Extensions;
using HelpDesk.Modules.Auth.Options;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Auth.Services.Implementations;

namespace HelpDesk.Modules.Auth;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddAuthModule()
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IAuthorizationGuard, AuthorizationGuard>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddKeyableOptions<DefaultTechnicianOptions>();
        }
    }
}