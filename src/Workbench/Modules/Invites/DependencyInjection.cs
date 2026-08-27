using Workbench.Modules.Invites.Repositories;
using Workbench.Modules.Invites.Repositories.Implementations;
using Workbench.Modules.Invites.Services;
using Workbench.Modules.Invites.Services.Implementations;

namespace Workbench.Modules.Invites;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddInvitesModule()
        {
            services.AddScoped<IInvitesService, InvitesService>();
            services.AddSingleton<ITokensService, TokensService>();
            services.AddScoped<IInvitesRepository, InvitesRepository>();
        }
    }
}
