using Microsoft.AspNetCore.Authorization;
using Workbench.Modules.Authorization.Handlers;
using Workbench.Modules.Authorization.Services;
using Workbench.Modules.Authorization.Services.Implementations;

namespace Workbench.Modules.Authorization;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddAuthorizationModule()
        {
            services.AddScoped<IAuthorizationGuard, AuthorizationGuard>();
            services.AddScoped<IAuthorizationHandler, OwnerHandler>();
            services.AddScoped<IAuthorizationHandler, OwnerOrLeadHandler>();
            services.AddScoped<IAuthorizationHandler, OwnerOrTeamMemberHandler>();
            services.AddScoped<IAuthorizationHandler, TeamMemberHandler>();
            services.AddScoped<IAuthorizationHandler, ProjectLeadHandler>();
            services.AddScoped<IAuthorizationHandler, AssignedOrLeadHandler>();

            services.AddAuthorization(options =>
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser().Build());
        }
    }
}