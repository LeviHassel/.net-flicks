using DotNetFlicks.Common.Configuration;
using DotNetFlicks.Managers.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DotNetFlicks.Managers.Managers
{
    //This class is used by the application to send email for account confirmation and password reset
    //For more details, see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailManager : IEmailManager
    {
        public EmailConfiguration _emailConfiguration { get; }

        public EmailManager(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient
            {
                Host = _emailConfiguration.MailServer,
                Port = _emailConfiguration.MailServerPort,
                Credentials = new NetworkCredential(_emailConfiguration.MailServerUsername, _emailConfiguration.MailServerPassword),
                EnableSsl = _emailConfiguration.EnableSsl
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_emailConfiguration.SenderEmail, _emailConfiguration.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mail.To.Add(email);
            client.Send(mail);
            client.Dispose();

            return Task.CompletedTask;
        }

        public Task SendEmailConfirmationAsync(string email, string link)
        {
            return SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking <a href='{HtmlEncoder.Default.Encode(link)}'>here</a>.");
        }
    }
}
