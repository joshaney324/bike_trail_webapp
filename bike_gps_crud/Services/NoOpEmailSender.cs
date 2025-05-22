using Microsoft.AspNetCore.Identity.UI.Services;

namespace bike_gps_crud.Services;

public class NoOpEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine($"Simulated email to {email} with subject '{subject}'");
        return Task.CompletedTask;
    }
}