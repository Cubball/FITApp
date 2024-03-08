using FITApp.IdentityService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FITApp.IdentityService.Data;

public static class DbInitializer
{
    public static async Task InitializeAdminAsync(IServiceScope scope, IConfiguration configuration)
    {
        using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        if (await userManager.Users.AnyAsync())
        {
            return;
        }

        var adminRoleName = configuration["AdminOptions:RoleName"] ?? throw new InvalidOperationException("AdminOptions:RoleName is not set");
        using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        var role = await roleManager.FindByNameAsync(adminRoleName);
        if (role is null)
        {
            role = new Role { Name = adminRoleName };
            var roleResult = await roleManager.CreateAsync(role);
            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException($"Failed to create admin role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }
            // TODO: add persmission
        }

        var adminEmail = configuration["AdminOptions:Email"] ?? throw new InvalidOperationException("AdminOptions:Email is not set");
        var adminPassword = configuration["AdminOptions:Password"] ?? throw new InvalidOperationException("AdminOptions:Password is not set");
        var adminUser = new User { UserName = adminEmail, Email = adminEmail };
        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        result = await userManager.AddToRoleAsync(adminUser, adminRoleName);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to assign user to admin role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
}