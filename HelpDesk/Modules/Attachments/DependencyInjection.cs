using HelpDesk.Modules.Attachments.Services;

namespace HelpDesk.Modules.Attachments;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddAttachmentsModule()
        {
            services.AddScoped<IAttachmentsReader, AttachmentsReader>();
            services.AddScoped<IAttachmentValidationService, AttachmentValidationService>();
        }
    }
}
