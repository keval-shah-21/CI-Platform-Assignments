namespace CI_Platform.Services.Service.Interface;

public interface IEmailService
{
    Task SendEmail(string Email, string Subject, string HtmlMessage);
}
