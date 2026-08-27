using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbench.Modules.Kanban.Models;

namespace Workbench.Modules.Kanban.Configuration;

public class BoardCardConfiguration : IEntityTypeConfiguration<BoardCard>
{
    public void Configure(EntityTypeBuilder<BoardCard> builder)
    {
        builder.Property(bc => bc.Position)
            .IsRequired();

        builder.HasIndex(bc => new { bc.ColumnId, bc.Position })
            .IsUnique();

        builder.HasOne(bc => bc.Column)
            .WithMany(c => c.Cards)
            .HasForeignKey(bc => bc.ColumnId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bc => bc.Issue)
            .WithMany()
            .HasForeignKey(bc => bc.IssueId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Board>()
            .WithMany()
            .HasForeignKey(bc => bc.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ensure that an issue can only appear once in a board, regardless of the column
        builder.HasIndex(bc => new { bc.BoardId, bc.IssueId })
            .IsUnique();
    }
}