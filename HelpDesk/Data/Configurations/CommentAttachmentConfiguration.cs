using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Data.Configurations;

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