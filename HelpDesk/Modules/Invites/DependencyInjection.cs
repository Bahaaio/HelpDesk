using HelpDesk.Modules.Invites.Services;
using HelpDesk.Modules.Invites.Services.Implementations;

namespace HelpDesk.Modules.Invites;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddInvitesModule()
        {
            services.AddScoped<IInvitesService, InvitesService>();
            services.AddSingleton<ITokensService, TokensService>();
        }
    }
}