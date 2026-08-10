using HelpDesk.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Data.Configurations;

public class AppDbContext(DbContextOptions options)
    : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>(options)
{
}