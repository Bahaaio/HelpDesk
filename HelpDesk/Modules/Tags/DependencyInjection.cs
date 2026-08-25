using HelpDesk.Modules.Tags.Repositories;
using HelpDesk.Modules.Tags.Repositories.Implementations;
using HelpDesk.Modules.Tags.Services;
using HelpDesk.Modules.Tags.Services.Implementations;

namespace HelpDesk.Modules.Tags;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddTagsModule()
        {
            services.AddScoped<ITagsService, TagsService>();
            services.AddScoped<ITagsRepository, TagsRepository>();
        }
    }
}