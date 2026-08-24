using HelpDesk.Modules.Issues.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Modules.Issues.Configuration;

public class IssueStatusChangeConfiguration : IEntityTypeConfiguration<IssueStatusChange>
{
    public void Configure(EntityTypeBuilder<IssueStatusChange> builder)
    {
        builder.Property(s => s.FromStatus)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(s => s.ToStatus)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(s => s.ChangedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(s => s.Issue)
            .WithMany(t => t.StatusChanges)
            .HasForeignKey(s => s.IssueId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.ChangedByUser)
            .WithMany()
            .HasForeignKey(s => s.ChangedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.IssueId);
    }
}