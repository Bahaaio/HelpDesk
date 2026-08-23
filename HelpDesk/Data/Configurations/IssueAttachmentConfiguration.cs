using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Data.Configurations;

public class IssueAttachmentConfiguration : IEntityTypeConfiguration<IssueAttachment>
{
    public void Configure(EntityTypeBuilder<IssueAttachment> builder)
    {
        builder.HasKey(ta => new { ta.OwnerId, ta.AttachmentId });

        builder.Property(ta => ta.OwnerId).HasColumnName("IssueId");

        builder.HasIndex(ta => ta.AttachmentId)
            .IsUnique();

        builder.HasOne(ta => ta.Issue)
            .WithMany(t => t.Attachments)
            .HasForeignKey(ta => ta.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ta => ta.Attachment)
            .WithMany()
            .HasForeignKey(ta => ta.AttachmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
