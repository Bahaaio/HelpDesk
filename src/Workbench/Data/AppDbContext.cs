using Workbench.Modules.Attachments.Models;
using Workbench.Modules.Auth.Models;
using Workbench.Modules.Comments.Models;
using Workbench.Modules.Invites.Models;
using Workbench.Modules.Issues.Models;
using Workbench.Modules.Issues.Votes.Models;
using Workbench.Modules.Tags.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Workbench.Data;

public class AppDbContext(DbContextOptions options)
    : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>(options)
{
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<InviteCode> InviteCodes { get; set; }
    public DbSet<IssueStatusChange> IssueStatusChanges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
