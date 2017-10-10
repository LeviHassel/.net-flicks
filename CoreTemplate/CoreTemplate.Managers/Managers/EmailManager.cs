using CoreTemplate.Managers.Interfaces;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CoreTemplate.Managers.Managers
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailManager : IEmailManager
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient("127.0.0.1", 25))
            {
                client.Send("test@coretemplate.com", email, subject, message);
            }

            return Task.CompletedTask;
        }
    }
}
