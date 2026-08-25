using HelpDesk.Data;
using HelpDesk.Data.Persistence;
using HelpDesk.Data.Persistence.Implementations;
using HelpDesk.Modules.Attachments;
using HelpDesk.Modules.Auth;
using HelpDesk.Modules.Authorization;
using HelpDesk.Modules.Comments;
using HelpDesk.Modules.Invites;
using HelpDesk.Modules.Issues;
using HelpDesk.Modules.Storage;
using HelpDesk.Modules.Tags;
using HelpDesk.Modules.Users;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Extensions;

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
        }
    }
}