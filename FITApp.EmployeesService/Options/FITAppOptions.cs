namespace FITApp.EmployeesService.Options;

public class FITAppOptions
{
    public const string SectionName = "FITAppOptions";
    
    public string PublicationsServiceBaseUrl { get; set; } = null!;

    public string PublicationsServiceUsersEndpoint { get; set; } = null!;
}