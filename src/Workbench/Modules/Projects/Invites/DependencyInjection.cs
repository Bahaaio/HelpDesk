using Workbench.Modules.Projects.Invites.Repositories;
using Workbench.Modules.Projects.Invites.Repositories.Implementations;
using Workbench.Modules.Projects.Invites.Services;
using Workbench.Modules.Projects.Invites.Services.Implementations;

namespace Workbench.Modules.Projects.Invites;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddProjectInvitesModule()
        {
            services.AddScoped<IProjectInvitesService, ProjectInvitesService>();
            services.AddSingleton<ITokensService, TokensService>();
            services.AddScoped<IProjectInvitesRepository, ProjectInvitesRepository>();
        }
    }
}