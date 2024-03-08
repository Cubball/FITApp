namespace FITApp.IdentityService.Infrastructure;

public interface IClock
{
    DateTime UtcNow { get; }
}