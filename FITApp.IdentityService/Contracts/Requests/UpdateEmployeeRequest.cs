namespace FITApp.IdentityService.Contracts.Requests;

public class UpdateEmployeeRequest
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string RoleId { get; set; } = null!;
}