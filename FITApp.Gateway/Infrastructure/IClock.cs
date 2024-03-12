namespace FITApp.Gateway.Infrastructure;

public interface IClock
{
    DateTime UtcNow { get; }
}