using Workbench.Modules.Projects.Memberships.Repositories;
using Workbench.Modules.Projects.Memberships.Repositories.Implementations;
using Workbench.Modules.Projects.Memberships.Services;
using Workbench.Modules.Projects.Memberships.Services.Implementations;

namespace Workbench.Modules.Projects.Memberships;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddProjectMembershipsModule()
        {
            services.AddScoped<IProjectMembershipsService, ProjectMembershipsService>();
            services.AddScoped<IProjectMembershipsRepository, ProjectMembershipsRepository>();
        }
    }
}