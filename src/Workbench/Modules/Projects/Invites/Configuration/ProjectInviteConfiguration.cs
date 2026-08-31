using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbench.Modules.Auth.Models;
using Workbench.Modules.Projects.Invites.Models;
using Workbench.Modules.Projects.Models;

namespace Workbench.Modules.Projects.Invites.Configuration;

public class ProjectInviteConfiguration : IEntityTypeConfiguration<ProjectInvite>
{
    public void Configure(EntityTypeBuilder<ProjectInvite> builder)
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

        builder.HasOne<Project>()
            .WithMany()
            .HasForeignKey(i => i.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(i => i.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(i => i.ProjectId);
        builder.HasIndex(i => i.CreatedById);
    }
}