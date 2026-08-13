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
            services.AddScoped<AuthService>();
            services.AddScoped<TicketsService>();
            services.AddScoped<CommentsService>();
            services.AddScoped<TagsService>();
            services.AddScoped<TicketTagsService>();
            services.AddScoped<VotesService>();
            services.AddScoped<UsersService>();
            services.AddScoped<StorageService>();
            services.AddScoped<AttachmentsService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
        }
    }
}