using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using FITApp.Auth.Attributes;
using FITApp.Auth.Data;
using FITApp.IdentityService.Contracts.Requests;
using FITApp.IdentityService.Entities;
using FITApp.IdentityService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

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
        // HACK: temporary hack for docker
        await context.Database.MigrateAsync();
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
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var appOptions = scope.ServiceProvider.GetRequiredService<IOptions<Options.FITAppOptions>>();
        if (await userManager.Users.AnyAsync())
        {
            return;
        }

        var adminRoleName = configuration["AdminOptions:RoleName"] ?? throw new InvalidOperationException("AdminOptions:RoleName is not set");
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
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
        var adminUser = new User { Id = Guid.NewGuid().ToString(), UserName = adminEmail, Email = adminEmail };
        var createEmployeeRequest = new CreateEmployeeRequest
        {
            Id = adminUser.Id,
            Email = adminUser.Email,
            Role = role.Name!,
            RoleId = role.Id,
        };
        var token = tokenService.GenerateJwt([new Claim("all", "true")]);
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(appOptions.Value.EmployeeServiceUsersEndpoint, UriKind.Relative),
            Headers =
            {
                { HeaderNames.Authorization, $"Bearer {token}" },
            },
            Content = new StringContent(JsonSerializer.Serialize(createEmployeeRequest), Encoding.UTF8, "application/json"),
        };
        using var httpClient = new HttpClient
        {
            BaseAddress = new Uri(appOptions.Value.EmployeeServiceBaseUrl),
        };
        var response = await httpClient.SendAsync(requestMessage);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Failed to create admin user in the Employee Service");
        }

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