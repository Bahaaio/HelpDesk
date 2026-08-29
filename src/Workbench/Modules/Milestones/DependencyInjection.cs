using Workbench.Modules.Milestones.Repositories;
using Workbench.Modules.Milestones.Repositories.Implementations;
using Workbench.Modules.Milestones.Services;
using Workbench.Modules.Milestones.Services.Implementations;

namespace Workbench.Modules.Milestones;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddMilestonesModule()
        {
            services.AddScoped<IMilestonesService, MilestonesService>();
            services.AddScoped<IMilestoneIssuesService, MilestoneIssuesService>();
            services.AddScoped<IMilestonesRepository, MilestonesRepository>();
        }
    }
}
