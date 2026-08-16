using HelpDesk.Api.Data;
using HelpDesk.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Extensions;

public static class ServiceExtensions
{
    extension(IServiceCollection services)
    {
        public void AddDatabaseServices(IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));
        }

        public void AddApplicationServices()
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITicketsService, TicketsService>();
            services.AddScoped<ITicketTagsService, TicketTagsService>();
            services.AddScoped<ITicketAssignmentsService, TicketAssignmentsService>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<ITagsService, TagsService>();
            services.AddScoped<IVotesService, VotesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IStorageService, LocalStorageService>();
            services.AddScoped<IAttachmentsService, AttachmentsService>();
            services.AddScoped<IAttachmentValidationService, AttachmentValidationService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IAuthorizationGuard, AuthorizationGuard>();
            services.AddSingleton<ITokensService, TokensService>();
            services.AddScoped<IInvitesService, InvitesService>();
        }
    }
}