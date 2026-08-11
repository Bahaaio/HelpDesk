using HelpDesk.Api.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Api.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

        string[] roles = [Role.Employee, Role.Technician];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }
}
