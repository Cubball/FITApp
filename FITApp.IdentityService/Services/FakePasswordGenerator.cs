namespace FITApp.IdentityService.Services
{
    public class FakePasswordGenerator : IPasswordGenerator
    {
        public string GeneratePassword()
        {
            return "password1234";
        }
    }
}