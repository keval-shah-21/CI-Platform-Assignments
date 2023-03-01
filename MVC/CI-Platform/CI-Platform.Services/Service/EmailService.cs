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
        emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html{Text = htmlMessage});

        using (var emailClient = new SmtpClient())
        {
            emailClient.Connect("smtp.gmail.com");
        }
        throw new NotImplementedException();
    }
}
