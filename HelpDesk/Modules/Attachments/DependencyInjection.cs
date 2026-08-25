using HelpDesk.Modules.Attachments.Repositories;
using HelpDesk.Modules.Attachments.Repositories.Implementations;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Attachments.Services.Implementations;

namespace HelpDesk.Modules.Attachments;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddAttachmentsModule()
        {
            services.AddScoped<IAttachmentsReader, AttachmentsReader>();
            services.AddScoped<IAttachmentValidationService, AttachmentValidationService>();

            services.AddScoped<IAttachmentsReadRepository, AttachmentsReadRepository>();
            services.AddScoped(typeof(IAttachmentsRepository<>), typeof(AttachmentsRepository<>));
        }
    }
}