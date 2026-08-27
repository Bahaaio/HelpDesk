using Workbench.Modules.Invites.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Workbench.Modules.Invites.Configuration;

public class InviteCodeConfiguration : IEntityTypeConfiguration<InviteCode>
{
    public void Configure(EntityTypeBuilder<InviteCode> builder)
    {
        builder.HasKey(i => i.Code);

        builder.Property(i => i.Code)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(i => i.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(i => i.ExpiresAt)
            .IsRequired();
    }
}
