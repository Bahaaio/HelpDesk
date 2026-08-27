using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Workbench.Modules.Attachments.Models;

namespace Workbench.Modules.Attachments.Configuration;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(a => a.ContentType)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(a => a.OriginalFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(a => a.Uploader)
            .WithMany()
            .HasForeignKey(a => a.UploaderId);

        builder.UseTphMappingStrategy();
    }
}
