using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDesk.Data.Configurations;

public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
{
    public void Configure(EntityTypeBuilder<TicketAttachment> builder)
    {
        builder.HasKey(ta => new { ta.OwnerId, ta.AttachmentId });

        builder.Property(ta => ta.OwnerId).HasColumnName("TicketId");

        builder.HasIndex(ta => ta.AttachmentId)
            .IsUnique();

        builder.HasOne(ta => ta.Ticket)
            .WithMany(t => t.Attachments)
            .HasForeignKey(ta => ta.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ta => ta.Attachment)
            .WithMany()
            .HasForeignKey(ta => ta.AttachmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
