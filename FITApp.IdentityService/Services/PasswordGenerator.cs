namespace FITApp.IdentityService.Services;

public class PasswordGenerator : IPasswordGenerator
{
    public string GeneratePassword()
    {
        return Guid.NewGuid().ToString().Substring(0, 8);
    }
}