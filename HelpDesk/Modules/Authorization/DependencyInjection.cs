using HelpDesk.Modules.Authorization.Handlers;
using HelpDesk.Modules.Authorization.Services;
using HelpDesk.Modules.Authorization.Services.Implementations;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Modules.Authorization;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddAuthorizationModule()
        {
            services.AddScoped<IAuthorizationGuard, AuthorizationGuard>();
            services.AddScoped<IAuthorizationHandler, OwnerOrTechnicianHandler>();

            services.AddAuthorization(options =>
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser().Build());
        }
    }
}