using HelpDesk.Data;
using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Auth;
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
        }

        /// <summary>
        ///     Registers every feature module.
        /// </summary>
        public void AddModules()
        {
            services.AddStorageModule();
            services.AddAttachmentsModule();
            services.AddAuthModule();
            services.AddUsersModule();
            services.AddInvitesModule();
            services.AddTagsModule();
            services.AddIssuesModule();
            services.AddCommentsModule();
        }
    }
}