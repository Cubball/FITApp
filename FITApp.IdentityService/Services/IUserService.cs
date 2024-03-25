using System.Security.Claims;

namespace FITApp.IdentityService.Services;

public interface IUserService
{
    Task<bool> IsUserValidAsync(string email, string password);

    Task<bool> TryChangePasswordAsync(string email, string oldPassword, string newPassword);

    Task<IEnumerable<Claim>> GetClaimsAsync(string email);

    Task<bool> IsRefreshTokenValidAsync(string email, string refreshToken);

    Task<bool> UpdateRefreshTokenAsync(string email, string refreshToken);

    Task<bool> TrySendResetPasswordEmailAsync(string email);

    Task<bool> TryResetPasswordAsync(string id, string token);
}