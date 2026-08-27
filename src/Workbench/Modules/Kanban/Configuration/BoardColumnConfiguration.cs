using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbench.Modules.Kanban.Models;

namespace Workbench.Modules.Kanban.Configuration;

public class BoardColumnConfiguration : IEntityTypeConfiguration<BoardColumn>
{
    public void Configure(EntityTypeBuilder<BoardColumn> builder)
    {
        builder.Property(bc => bc.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(bc => bc.Description)
            .HasMaxLength(512);

        builder.Property(bc => bc.Position)
            .IsRequired();

        builder.HasIndex(bc => new { bc.BoardId, bc.Position })
            .IsUnique();

        builder.Property(bc => bc.Color)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(bc => bc.Board)
            .WithMany(b => b.Columns)
            .HasForeignKey(bc => bc.BoardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}