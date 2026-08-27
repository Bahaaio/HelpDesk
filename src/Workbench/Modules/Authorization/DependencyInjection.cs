using Workbench.Modules.Authorization.Handlers;
using Workbench.Modules.Authorization.Services;
using Workbench.Modules.Authorization.Services.Implementations;
using Microsoft.AspNetCore.Authorization;

namespace Workbench.Modules.Authorization;

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
