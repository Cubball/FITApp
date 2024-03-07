using System.Security.Claims;

namespace FITApp.IdentityService.Services;

public interface IUserService
{
    Task<bool> IsUserValidAsync(string email, string password);

    Task<bool> TryChangePasswordAsync(string email, string oldPassword, string newPassword);

    Task<IEnumerable<Claim>> GetClaimsAsync(string email);

    Task<string?> GetRefreshTokenAsync(string email);

    Task<bool> UpdateRefreshTokenAsync(string email, string refreshToken);
}