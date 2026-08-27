using Workbench.Common.Extensions;
using Workbench.Data;
using Workbench.Modules.Auth.Models;
using Workbench.Modules.Auth.Options;
using Workbench.Modules.Auth.Services;
using Workbench.Modules.Auth.Services.Implementations;
using Microsoft.AspNetCore.Identity;

namespace Workbench.Modules.Auth;

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
                options.Cookie.Name = "Workbench.Auth";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;

                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
            });
        }
    }
}
