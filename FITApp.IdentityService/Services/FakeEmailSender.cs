namespace FITApp.IdentityService.Services;

public class FakeEmailSender : IEmailSender
{
    public Task SendEmail(string email, string subject, string messageBody)
    {
        Console.WriteLine(@$"Sending email to ""{email}"" with subject ""{subject}"" and body ""{messageBody}""");
        return Task.CompletedTask;
    }
}