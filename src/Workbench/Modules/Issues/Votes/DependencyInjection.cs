using Workbench.Modules.Issues.Votes.Repositories;
using Workbench.Modules.Issues.Votes.Repositories.Implementations;
using Workbench.Modules.Issues.Votes.Services;
using Workbench.Modules.Issues.Votes.Services.Implementations;

namespace Workbench.Modules.Issues.Votes;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddIssueVotesModule()
        {
            services.AddScoped<IVotesService, VotesService>();
            services.AddScoped<IVotesRepository, VotesRepository>();
        }
    }
}