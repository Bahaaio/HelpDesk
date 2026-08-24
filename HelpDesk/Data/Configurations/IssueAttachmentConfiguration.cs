using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Data.Configurations;

public class IssueAttachmentConfiguration : IEntityTypeConfiguration<IssueAttachment>
{
    public void Configure(EntityTypeBuilder<IssueAttachment> builder)
    {
        builder.Property(ta => ta.OwnerId)
            .HasColumnName("IssueId");

        builder.HasOne(ta => ta.Issue)
            .WithMany(i => i.Attachments)
            .HasForeignKey(ta => ta.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}