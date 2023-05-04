namespace CI_Platform.Services.Service.Interface;

public interface IEmailService
{
    Task SendEmail(string email, string subject, string htmlMessage);
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
