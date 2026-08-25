using HelpDesk.Common.Extensions;
using HelpDesk.Data;
using HelpDesk.Modules.Auth.Models;
using HelpDesk.Modules.Auth.Options;
using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Auth.Services.Implementations;
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Modules.Auth;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddAuthenticationModule()
        {
            services.AddIdentityServices();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddKeyableOptions<DefaultTechnicianOptions>();
        }

        private void AddIdentityServices()
        {
            services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddIdentityCookies();

            services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;

                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole<int>>()
                .AddSignInManager()
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "HelpDesk.Auth";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;

                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
            });
        }
    }
}