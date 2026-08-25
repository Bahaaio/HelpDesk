using HelpDesk.Common.Extensions;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Comments.Models;
using HelpDesk.Modules.Comments.Options;
using HelpDesk.Modules.Comments.Repositories;
using HelpDesk.Modules.Comments.Repositories.Implementations;
using HelpDesk.Modules.Comments.Services;
using HelpDesk.Modules.Comments.Services.Implementations;

namespace HelpDesk.Modules.Comments;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddCommentsModule()
        {
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<IAttachmentsService<Comment>, CommentAttachmentsService>();

            services.AddScoped<ICommentsRepository, CommentsRepository>();

            services.AddKeyableOptions<CommentAttachmentOptions>();
        }
    }
}