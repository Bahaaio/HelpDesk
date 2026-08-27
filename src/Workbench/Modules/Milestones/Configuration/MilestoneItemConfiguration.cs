using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbench.Modules.Milestones.Models;

namespace Workbench.Modules.Milestones.Configuration;

public class MilestoneItemConfiguration : IEntityTypeConfiguration<MilestoneItem>
{
    public void Configure(EntityTypeBuilder<MilestoneItem> builder)
    {
        builder.HasKey(mi => new { mi.MilestoneId, mi.IssueId });

        builder.HasOne(mi => mi.Milestone)
            .WithMany(m => m.MilestoneItems)
            .HasForeignKey(mi => mi.MilestoneId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mi => mi.Issue)
            .WithMany()
            .HasForeignKey(mi => mi.IssueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}