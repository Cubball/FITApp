using System.Security.Claims;
using FITApp.Auth;
using FITApp.IdentityService.Entities;
using FITApp.IdentityService.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FITApp.IdentityService.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IClock _clock;

    public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IClock clock)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _clock = clock;
    }

    public async Task<bool> IsUserValidAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user is not null && await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> TryChangePasswordAsync(string email, string oldPassword, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return false;
        }

        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        return result.Succeeded;
    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return [];
        }

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
        };
        var roles = await _userManager.GetRolesAsync(user);
        var roleName = roles.FirstOrDefault();
        if (roleName is not null)
        {
            claims.Add(new Claim(CustomClaimTypes.Role, roleName));
            var role = await _roleManager.FindByNameAsync(roleName);
            var roleClaims = await _roleManager.GetClaimsAsync(role!);
            claims.AddRange(roleClaims);
        }

        return claims;
    }

    public async Task<bool> IsRefreshTokenValidAsync(string email, string refreshToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user?.RefreshToken == refreshToken && user?.RefreshTokenExpiryTime > _clock.UtcNow;
    }

    public async Task<bool> UpdateRefreshTokenAsync(string email, string refreshToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return false;
        }

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = _clock.UtcNow.AddDays(15);
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
}