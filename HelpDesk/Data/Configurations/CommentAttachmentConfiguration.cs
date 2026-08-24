using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Data.Configurations;

public class CommentAttachmentConfiguration : IEntityTypeConfiguration<CommentAttachment>
{
    public void Configure(EntityTypeBuilder<CommentAttachment> builder)
    {
        builder.Property(ta => ta.OwnerId)
            .HasColumnName("CommentId");

        builder.HasOne(ta => ta.Comment)
            .WithMany(c => c.Attachments)
            .HasForeignKey(ta => ta.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}