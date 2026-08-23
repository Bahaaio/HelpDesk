using HelpDesk.Data;
using HelpDesk.Models;
using HelpDesk.Services.Attachments;
using HelpDesk.Services.Auth;
using HelpDesk.Services.Comments;
using HelpDesk.Services.Invites;
using HelpDesk.Services.Storage;
using HelpDesk.Services.Tags;
using HelpDesk.Services.Issues;
using HelpDesk.Services.Users;
using HelpDesk.Services.Votes;
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

        public void AddApplicationServices()
        {
            // auth
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IAuthorizationGuard, AuthorizationGuard>();
            services.AddScoped<IInvitesService, InvitesService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<ITokensService, TokensService>();

            // attachments
            services.AddScoped<IAttachmentsReader, AttachmentsReader>();
            services.AddScoped<IAttachmentValidationService, AttachmentValidationService>();
            services.AddScoped<IAttachmentsService<Issue>, IssueAttachmentsService>();
            services.AddScoped<IAttachmentsService<Comment>, CommentAttachmentsService>();

            // issues
            services.AddScoped<IIssuesService, IssuesService>();
            services.AddScoped<IIssueTagsService, IssueTagsService>();
            services.AddScoped<IIssueAssignmentsService, IssueAssignmentsService>();
            services.AddScoped<IIssueStatusService, IssueStatusService>();

            // misc
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<ITagsService, TagsService>();
            services.AddScoped<IVotesService, VotesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IStorageService, LocalStorageService>();
        }
    }
}