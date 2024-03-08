using Microsoft.IdentityModel.JsonWebTokens;

namespace FITApp.Auth.Data;

/// <summary>
/// Contains default values for the TokenValidationParameters that are used when adding JWT auth.
/// </summary>
public static class DefaultTokenValidationParametersValues
{
    public static readonly TimeSpan ClockSkew = new(0, 0, 5);
    public const bool ValidateIssuer = false; // NOTE: may change later
    public const bool ValidateAudience = false;
    public const bool ValidateLifetime = true;
    public const bool ValidateIssuerSigningKey = true;
    public const string NameClaimType = JwtRegisteredClaimNames.Name;
    public const string RoleClaimType = CustomClaimTypes.Role;
}