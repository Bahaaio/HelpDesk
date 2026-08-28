using Workbench.Data;
using Workbench.Data.Persistence;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Attachments;
using Workbench.Modules.Auth;
using Workbench.Modules.Authorization;
using Workbench.Modules.Comments;
using Workbench.Modules.Invites;
using Workbench.Modules.Issues;
using Workbench.Modules.Storage;
using Workbench.Modules.Tags;
using Workbench.Modules.Users;
using Microsoft.EntityFrameworkCore;
using Workbench.Modules.Projects;

namespace Workbench.Extensions;

public static class ServiceExtensions
{
    extension(IServiceCollection services)
    {
        public void AddDatabaseServices(IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        ///     Registers every feature module.
        /// </summary>
        public void AddModules()
        {
            services.AddStorageModule();
            services.AddAttachmentsModule();
            services.AddAuthenticationModule();
            services.AddAuthorizationModule();
            services.AddUsersModule();
            services.AddInvitesModule();
            services.AddTagsModule();
            services.AddIssuesModule();
            services.AddCommentsModule();
            services.AddProjectsModule();
        }
    }
}
