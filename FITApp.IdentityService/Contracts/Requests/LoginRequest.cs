using System.ComponentModel.DataAnnotations;

namespace FITApp.IdentityService.Contracts.Requests;

public class LoginRequest
{
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}