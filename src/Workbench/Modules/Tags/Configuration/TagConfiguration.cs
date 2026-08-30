using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbench.Modules.Tags.Models;

namespace Workbench.Modules.Tags.Configuration;

internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(t => new { t.ProjectId, t.Name })
            .IsUnique();

        builder.Property(t => t.Description)
            .HasMaxLength(2000);

        builder.Property(t => t.Color)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tags)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Issues)
            .WithMany(t => t.Tags);
    }
}