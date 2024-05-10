using System.Security.Claims;
using FITApp.Auth.Data;
using FITApp.IdentityService.Entities;
using FITApp.IdentityService.Infrastructure;
using FITApp.IdentityService.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FITApp.IdentityService.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IEmailSender _emailSender;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IClock _clock;
    private readonly FITAppOptions _options;

    public UserService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IEmailSender emailSender,
        IPasswordGenerator passwordGenerator,
        IClock clock,
        IOptions<FITAppOptions> options)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _emailSender = emailSender;
        _passwordGenerator = passwordGenerator;
        _clock = clock;
        _options = options.Value;
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
        if (result.Succeeded)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userManager.UpdateAsync(user);
        }

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

    public async Task<bool> TrySendResetPasswordEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return false;
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callbackUrl = $"{_options.BaseUrl}/reset-password-confirm?id={user.Id}&token={token}";
        try
        {
            await _emailSender.SendEmail(email, "Скидання пароля у FITApp", $"Щоб скинути Ваш пароль, натисніть перейдіть за даним посиланням: {callbackUrl}");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> TryResetPasswordAsync(string id, string token)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return false;
        }

        var password = _passwordGenerator.GeneratePassword();
        var result = await _userManager.ResetPasswordAsync(user, token, password);
        Console.WriteLine(result.Succeeded);
        if (!result.Succeeded)
        {
            return false;
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await _userManager.UpdateAsync(user);
        try
        {
            await _emailSender.SendEmail(user.Email!, "Новий пароль у FITApp", $"Ваш новий пароль: {password}");
            return true;
        }
        catch
        {
            return false;
        }
    }
}