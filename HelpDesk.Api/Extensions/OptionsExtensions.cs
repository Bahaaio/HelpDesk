using HelpDesk.Api.Options;

namespace HelpDesk.Api.Extensions;

public static class OptionsExtensions
{
    extension(IServiceCollection services)
    {
        public void AddOptionsServices()
        {
            services.AddKeyableOptions<DefaultTechnicianOptions>();
            services.AddKeyableOptions<TicketAttachmentOptions>();
        }

        private void AddKeyableOptions<T>() where T : class, IKeyableOptions
        {
            services.AddOptions<T>()
                .BindConfiguration(T.Key)
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }
    }
}