using Workbench.Modules.Comments.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Workbench.Modules.Comments.Configuration;

public class CommentAttachmentConfiguration : IEntityTypeConfiguration<CommentAttachment>
{
    public void Configure(EntityTypeBuilder<CommentAttachment> builder)
    {
        builder.Property(ta => ta.ParentId)
            .HasColumnName("CommentId");

        builder.HasOne(ta => ta.Comment)
            .WithMany(c => c.Attachments)
            .HasForeignKey(ta => ta.ParentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
