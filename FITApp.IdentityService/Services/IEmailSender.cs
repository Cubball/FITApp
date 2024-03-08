namespace FITApp.IdentityService.Services;

public interface IEmailSender
{
    Task SendEmail(string email, string subject, string messageBody);
}