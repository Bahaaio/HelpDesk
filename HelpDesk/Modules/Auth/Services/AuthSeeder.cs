using HelpDesk.Modules.Auth.Enums;
using HelpDesk.Modules.Auth.Options;
using HelpDesk.Modules.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HelpDesk.Modules.Auth.Services;

/// <summary>
///     Seeds identity-related data: roles and the default technician account.
/// </summary>
public static class AuthSeeder
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        await SeedRolesAsync(services);
        await SeedDefaultTechnicianAsync(services);
    }

    private static async Task SeedRolesAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        string[] roles = [Role.Employee, Role.Technician];

        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(role));
                logger.LogInformation("Role created: {Role}", role);
            }
    }

    private static async Task SeedDefaultTechnicianAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var logger = services.GetRequiredService<ILogger<Program>>();
        var options = services.GetRequiredService<IOptions<DefaultTechnicianOptions>>().Value;

        if (await userManager.FindByNameAsync(options.Username) is not null)
        {
            logger.LogInformation("Default technician already exists");
            return;
        }

        var technician = new ApplicationUser
        {
            UserName = options.Username,
            Email = options.Email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(technician, options.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(technician, Role.Technician);
            logger.LogInformation("Default technician created: {Username}", options.Username);
        }
    }
}