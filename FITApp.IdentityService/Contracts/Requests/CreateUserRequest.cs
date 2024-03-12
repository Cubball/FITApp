using System.ComponentModel.DataAnnotations;

namespace FITApp.IdentityService.Contracts.Requests;

public class CreateUserRequest
{
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string RoleId { get; set; } = null!;
}