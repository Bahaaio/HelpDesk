using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Comments.Models;
using HelpDesk.Modules.Invites.Models;
using HelpDesk.Modules.Issues.Models;
using HelpDesk.Modules.Issues.Votes;
using HelpDesk.Modules.Tags.Models;
using HelpDesk.Modules.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Data;

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