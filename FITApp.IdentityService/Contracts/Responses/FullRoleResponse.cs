namespace FITApp.IdentityService.Contracts.Responses;

public class FullRoleResponse
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public IEnumerable<string> Permissions { get; set; } = null!;
}