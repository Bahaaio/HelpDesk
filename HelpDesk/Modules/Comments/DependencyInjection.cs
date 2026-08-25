using HelpDesk.Extensions;
using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Comments.Models;
using HelpDesk.Modules.Comments.Options;
using HelpDesk.Modules.Comments.Services;

namespace HelpDesk.Modules.Comments;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddCommentsModule()
        {
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<IAttachmentsService<Comment>, CommentAttachmentsService>();

            services.AddKeyableOptions<CommentAttachmentOptions>();
        }
    }
}
