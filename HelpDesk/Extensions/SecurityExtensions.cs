using HelpDesk.Authorization.Handlers;
using HelpDesk.Data;
using HelpDesk.Models;
using HelpDesk.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Extensions;

public static class SecurityExtensions
{
    extension(IServiceCollection services)
    {
        public void AddIdentityServices()
        {
            services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

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

            services.AddScoped<AuthenticationStateProvider, HttpContextAuthenticationStateProvider>();
        }

        public void AddAuthorizationServices()
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser().Build();
            });

            services.AddScoped<IAuthorizationHandler, OwnerOrTechnicianHandler>();
        }
    }
}
