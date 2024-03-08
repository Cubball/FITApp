using System.Reflection;
using System.Security.Claims;
using FITApp.Auth;
using FITApp.IdentityService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FITApp.IdentityService.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceScope scope, IConfiguration configuration)
    {
        await InitializePermissionsAsync(scope);
        await InitializeAdminAsync(scope, configuration);
    }

    private static async Task InitializePermissionsAsync(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var permissionInDb = await context.Permissions.ToListAsync();
        var permissionsClassType = typeof(Permissions);
        var permissions = permissionsClassType
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.IsLiteral && f.GetCustomAttribute<IgnoreAttribute>() is null)
            .Select(f => new Permission
            {
                Name = (string)f.GetValue(null)!,
                Description = f.GetCustomAttribute<PermissionDescriptionAttribute>()?.Description
                           ?? throw new InvalidOperationException($"Permission {f.Name} does not have a description")
            })
            .ToList();
        var missingPermissions = permissions.Where(p => permissionInDb.All(pdb => pdb.Name != p.Name)).ToList();
        var extraPermissions = permissionInDb.Where(pdb => permissions.All(p => p.Name != pdb.Name)).ToList();
        context.Permissions.RemoveRange(extraPermissions);
        context.Permissions.AddRange(missingPermissions);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Failed to initialize permissions", e);
        }
    }

    private static async Task InitializeAdminAsync(IServiceScope scope, IConfiguration configuration)
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
            role = new Role { Name = adminRoleName, IsAssignable = false };
            var roleResult = await roleManager.CreateAsync(role);
            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException($"Failed to create admin role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }

            roleResult = await roleManager.AddClaimAsync(role, new Claim(Permissions.All, "true"));
            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException($"Failed to add claim to admin role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }
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