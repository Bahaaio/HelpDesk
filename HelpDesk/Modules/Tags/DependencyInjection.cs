using HelpDesk.Modules.Tags.Services;

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