using System.Security.Claims;
using System.Security.Cryptography;
using FITApp.Auth.Data;
using FITApp.IdentityService.Infrastructure;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace FITApp.IdentityService.Services;

public class TokenService : ITokenService
{
    private readonly RsaSecurityKey _key;
    private readonly IClock _clock;

    public TokenService(string key, IClock clock)
    {
        var bytes = Convert.FromBase64String(key);
        var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(bytes, out _);
        _key = new RsaSecurityKey(rsa);
        _clock = clock;
    }

    public string GenerateJwt(IEnumerable<Claim> claims)
    {
        var tokenDescriptior = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            IssuedAt = _clock.UtcNow,
            Expires = _clock.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.RsaSha256),
        };
        var tokenHandler = new JsonWebTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptior);
        return token;
    }

    public string GenerateRefreshToken()
    {
        using var random = RandomNumberGenerator.Create();
        Span<byte> bytes = stackalloc byte[128];
        random.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public async Task<(IEnumerable<Claim>, bool)> GetClaimsFromExpiredTokenAsync(string token)
    {
        var tokenHandler = new JsonWebTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = _key,
            ValidateLifetime = false,
            ClockSkew = DefaultTokenValidationParametersValues.ClockSkew,
            ValidateIssuer = DefaultTokenValidationParametersValues.ValidateIssuer,
            ValidateAudience = DefaultTokenValidationParametersValues.ValidateAudience,
            ValidateIssuerSigningKey = DefaultTokenValidationParametersValues.ValidateIssuerSigningKey,
            NameClaimType = DefaultTokenValidationParametersValues.NameClaimType,
            RoleClaimType = DefaultTokenValidationParametersValues.RoleClaimType,
        };
        var result = await tokenHandler.ValidateTokenAsync(token, validationParameters);
        return result.IsValid
            ? (result.ClaimsIdentity.Claims, true)
            : ([], false);
    }
}