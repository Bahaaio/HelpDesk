using HelpDesk.ClientServices;
using HelpDesk.ClientServices.Implementations;
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
            services.AddScoped<IAuthState, AuthState>();
            services.AddScoped<IIssueAttachmentsClient, IssueAttachmentsClient>();
            services.AddScoped<ICommentAttachmentsClient, CommentAttachmentsClient>();
        }
    }
}