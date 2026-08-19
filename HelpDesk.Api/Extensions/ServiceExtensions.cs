using HelpDesk.Api.Data;
using HelpDesk.Api.Models;
using HelpDesk.Api.Services.Attachments;
using HelpDesk.Api.Services.Auth;
using HelpDesk.Api.Services.Comments;
using HelpDesk.Api.Services.Invites;
using HelpDesk.Api.Services.Storage;
using HelpDesk.Api.Services.Tags;
using HelpDesk.Api.Services.Tickets;
using HelpDesk.Api.Services.Users;
using HelpDesk.Api.Services.Votes;
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
            // auth
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IAuthorizationGuard, AuthorizationGuard>();
            services.AddSingleton<ITokensService, TokensService>();
            services.AddScoped<IInvitesService, InvitesService>();
            services.AddScoped<IAuthService, AuthService>();

            // attachments
            services.AddScoped<IAttachmentsReader, AttachmentsReader>();
            services.AddScoped<IAttachmentValidationService, AttachmentValidationService>();
            services.AddScoped<IAttachmentsService<Ticket>, TicketAttachmentsService>();

            // tickets
            services.AddScoped<ITicketsService, TicketsService>();
            services.AddScoped<ITicketTagsService, TicketTagsService>();
            services.AddScoped<ITicketAssignmentsService, TicketAssignmentsService>();
            services.AddScoped<ITicketStatusService, TicketStatusService>();

            // misc
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<ITagsService, TagsService>();
            services.AddScoped<IVotesService, VotesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IStorageService, LocalStorageService>();
        }
    }
}