using System.Security.Claims;

namespace FITApp.IdentityService.Services;

public interface ITokenService
{
    string GenerateJwt(IEnumerable<Claim> claims);

    string GenerateRefreshToken();

    Task<(IEnumerable<Claim> Claims, bool IsValid)> GetClaimsFromExpiredToken(string token);
}