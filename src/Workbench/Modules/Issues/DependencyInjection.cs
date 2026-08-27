using Workbench.Common.Extensions;
using Workbench.Modules.Attachments.Services;
using Workbench.Modules.Issues.Models;
using Workbench.Modules.Issues.Options;
using Workbench.Modules.Issues.Repositories;
using Workbench.Modules.Issues.Repositories.Implementations;
using Workbench.Modules.Issues.Services;
using Workbench.Modules.Issues.Services.Implementations;
using Workbench.Modules.Issues.Votes.Repositories;
using Workbench.Modules.Issues.Votes.Repositories.Implementations;
using Workbench.Modules.Issues.Votes.Services;
using Workbench.Modules.Issues.Votes.Services.Implementations;

namespace Workbench.Modules.Issues;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddIssuesModule()
        {
            services.AddScoped<IIssuesService, IssuesService>();
            services.AddScoped<IIssueTagsService, IssueTagsService>();
            services.AddScoped<IIssueAssignmentsService, IssueAssignmentsService>();
            services.AddScoped<IIssueStatusService, IssueStatusService>();
            services.AddScoped<IAttachmentsService<Issue>, IssueAttachmentsService>();
            services.AddScoped<IVotesService, VotesService>();

            services.AddScoped<IIssuesRepository, IssuesRepository>();
            services.AddScoped<IIssueStatusChangeRepository, IssueStatusChangeRepository>();
            services.AddScoped<IVotesRepository, VotesRepository>();

            services.AddKeyableOptions<IssueAttachmentOptions>();
        }
    }
}
