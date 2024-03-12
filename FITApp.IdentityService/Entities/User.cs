using Microsoft.AspNetCore.Identity;

namespace FITApp.IdentityService.Entities;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; } = null!;

    public DateTime? RefreshTokenExpiryTime { get; set; }
}