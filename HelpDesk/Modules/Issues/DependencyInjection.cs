using HelpDesk.Common.Extensions;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Issues.Models;
using HelpDesk.Modules.Issues.Options;
using HelpDesk.Modules.Issues.Repositories;
using HelpDesk.Modules.Issues.Repositories.Implementations;
using HelpDesk.Modules.Issues.Services;
using HelpDesk.Modules.Issues.Services.Implementations;
using HelpDesk.Modules.Issues.Votes.Repositories;
using HelpDesk.Modules.Issues.Votes.Repositories.Implementations;
using HelpDesk.Modules.Issues.Votes.Services;
using HelpDesk.Modules.Issues.Votes.Services.Implementations;

namespace HelpDesk.Modules.Issues;

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