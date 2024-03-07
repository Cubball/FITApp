namespace FITApp.IdentityService.Contracts.Responses;

public class RefreshResponse
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}