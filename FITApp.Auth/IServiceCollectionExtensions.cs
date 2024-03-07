using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FITApp.Auth;

public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds JWT authentication and authorization with predefined <see cref="DefaultTokenValidationParametersValues"/> and the provided <paramref name="publicKey" />.
    /// </summary>
    /// <param name="publicKey">Base64 encoded public RSA key</param>
    public static IServiceCollection AddJWTAuth(this IServiceCollection services, string publicKey)
    {
        var bytes = Convert.FromBase64String(publicKey);
        var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(bytes, out _);
        var rsaSecurityKey = new RsaSecurityKey(rsa);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = DefaultTokenValidationParametersValues.ClockSkew,
                ValidateIssuer = DefaultTokenValidationParametersValues.ValidateIssuer,
                ValidateAudience = DefaultTokenValidationParametersValues.ValidateAudience,
                ValidateLifetime = DefaultTokenValidationParametersValues.ValidateLifetime,
                ValidateIssuerSigningKey = DefaultTokenValidationParametersValues.ValidateIssuerSigningKey,
                NameClaimType = DefaultTokenValidationParametersValues.NameClaimType,
                RoleClaimType = DefaultTokenValidationParametersValues.RoleClaimType,
                IssuerSigningKey = rsaSecurityKey,
            });
        services.AddAuthorization();
        return services;
    }
}