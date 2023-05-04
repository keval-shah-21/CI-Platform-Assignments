using CI_Platform.Services.Service.Interface;
using MimeKit;
using MailKit.Net.Smtp;

namespace CI_Platform.Services.Service;

public class EmailService : IEmailService
{
    public Task SendEmail(string email, string subject, string htmlMessage)
    {
        var emailToSend = new MimeMessage();
        emailToSend.From.Add(MailboxAddress.Parse("keval.itaims@gmail.com"));
        emailToSend.To.Add(MailboxAddress.Parse(email));
        emailToSend.Subject = subject;
        emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html){Text = htmlMessage};

        using (var emailClient = new SmtpClient())
        {
            emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            emailClient.Authenticate("keval.itaims@gmail.com", "sxmffstquwixzuht");
            emailClient.Send(emailToSend);
            emailClient.Disconnect(true);
        }
        return Task.CompletedTask;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailToSend = new MimeMessage();
        emailToSend.From.Add(MailboxAddress.Parse("keval.itaims@gmail.com"));
        emailToSend.To.Add(MailboxAddress.Parse(email));
        emailToSend.Subject = subject;
        emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        using (var emailClient = new SmtpClient())
        {
            await emailClient.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await emailClient.AuthenticateAsync("keval.itaims@gmail.com", "sxmffstquwixzuht");
            await emailClient.SendAsync(emailToSend);
            await emailClient.DisconnectAsync(true);
        }
    }
}
