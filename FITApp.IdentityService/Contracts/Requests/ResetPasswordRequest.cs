using System.ComponentModel.DataAnnotations;

namespace FITApp.IdentityService.Contracts.Requests;

public class ResetPasswordRequest
{
    [EmailAddress]
    public string Email { get; set; } = null!;
}