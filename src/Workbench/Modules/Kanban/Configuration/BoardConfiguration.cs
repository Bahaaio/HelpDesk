using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbench.Modules.Kanban.Models;

namespace Workbench.Modules.Kanban.Configuration;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(b => b.Project)
            .WithOne(p => p.Board)
            .HasForeignKey<Board>(b => b.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}