using HelpDesk.ClientServices;
using MudBlazor.Services;

namespace HelpDesk.Extensions;

public static class UiExtensions
{
    extension(IServiceCollection services)
    {
        public void AddUiServices()
        {
            services.AddRazorComponents().AddInteractiveServerComponents();
            services.AddMudServices();
            services.AddScoped<AuthState>();
            services.AddHttpClient<ITicketAttachmentsClient, TicketAttachmentsClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5258");
            });
            services.AddScoped<ITicketAttachmentsClient, TicketAttachmentsClient>();
        }
    }
}