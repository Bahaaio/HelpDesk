using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Modules.Issues.Votes;

public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasKey(v => new { v.IssueId, v.VoterId });

        builder.Property(v => v.Value)
            .IsRequired();

        builder.Property(v => v.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(v => v.Issue)
            .WithMany(t => t.Votes)
            .HasForeignKey(v => v.IssueId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(v => v.Voter)
            .WithMany(u => u.Votes)
            .HasForeignKey(v => v.VoterId);
    }
}