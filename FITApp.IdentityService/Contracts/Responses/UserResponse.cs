namespace FITApp.IdentityService.Contracts.Responses;

public class UserResponse
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public string RoleName { get; set; } = null!;
}