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
        }
    }
}