namespace FITApp.IdentityService.Options;

public class JwtOptions
{
    public const string SectionKey = "JwtOptions";

    public string PublicKey { get; set; } = null!;

    public string PrivateKey { get; set; } = null!;
}