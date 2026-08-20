using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Data.Configurations;

public class CommentAttachmentConfiguration : IEntityTypeConfiguration<CommentAttachment>
{
    public void Configure(EntityTypeBuilder<CommentAttachment> builder)
    {
        builder.HasKey(ca => new { ca.OwnerId, ca.AttachmentId });

        builder.Property(ta => ta.OwnerId).HasColumnName("CommentId");

        builder.HasIndex(ta => ta.AttachmentId)
            .IsUnique();

        builder.HasOne(ta => ta.Attachment)
            .WithMany()
            .HasForeignKey(ta => ta.AttachmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ta => ta.Comment)
            .WithMany()
            .HasForeignKey(ta => ta.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}