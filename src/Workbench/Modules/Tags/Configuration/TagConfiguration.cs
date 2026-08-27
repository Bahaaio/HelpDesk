using Workbench.Modules.Tags.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Workbench.Modules.Tags.Configuration;

internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(t => t.Name)
            .IsUnique();

        builder.Property(t => t.Description)
            .HasMaxLength(2000);

        builder.HasMany(t => t.Issues)
            .WithMany(t => t.Tags);
    }
}
