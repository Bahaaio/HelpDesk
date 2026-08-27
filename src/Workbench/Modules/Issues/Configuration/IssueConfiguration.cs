using Workbench.Modules.Issues.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Workbench.Modules.Issues.Configuration;

public class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(t => t.Description)
            .HasMaxLength(2000);

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(t => t.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(t => t.Author)
            .WithMany(a => a.CreatedIssues)
            .HasForeignKey(t => t.AuthorId);

        builder.HasOne(t => t.AssignedTo)
            .WithMany(u => u.AssignedIssues)
            .HasForeignKey(t => t.AssignedToId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(t => t.AssignedToId);
    }
}
