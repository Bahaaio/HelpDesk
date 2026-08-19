using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Data.Configurations;

public class TicketStatusChangeConfiguration : IEntityTypeConfiguration<TicketStatusChange>
{
    public void Configure(EntityTypeBuilder<TicketStatusChange> builder)
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

        builder.HasOne(s => s.Ticket)
            .WithMany(t => t.StatusChanges)
            .HasForeignKey(s => s.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.ChangedByUser)
            .WithMany()
            .HasForeignKey(s => s.ChangedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.TicketId);
    }
}
