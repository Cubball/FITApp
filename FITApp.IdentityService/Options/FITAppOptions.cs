namespace FITApp.IdentityService.Options;

public class FITAppOptions
{
    public const string SectionName = "FITAppOptions";

    public string BaseUrl { get; set; } = null!;

    public string EmployeeServiceBaseUrl { get; set; } = null!;

    public string EmployeeServiceUsersEndpoint { get; set; } = null!;
}