using HelpDesk.Modules.Issues.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Modules.Issues.Configuration;

public class IssueAttachmentConfiguration : IEntityTypeConfiguration<IssueAttachment>
{
    public void Configure(EntityTypeBuilder<IssueAttachment> builder)
    {
        builder.Property(ta => ta.ParentId)
            .HasColumnName("IssueId");

        builder.HasOne(ta => ta.Issue)
            .WithMany(i => i.Attachments)
            .HasForeignKey(ta => ta.ParentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}