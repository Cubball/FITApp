namespace FITApp.IdentityService.Contracts.Requests;

public class CreateRoleRequest
{
    public string Name { get; set; } = null!;

    public IEnumerable<string> Permissions { get; set; } = null!;
}