namespace FITApp.IdentityService.Contracts.Requests;

public class UpdateRoleRequest
{
    public string Name { get; set; } = null!;

    public IEnumerable<string> Permissions { get; set; } = null!;
}