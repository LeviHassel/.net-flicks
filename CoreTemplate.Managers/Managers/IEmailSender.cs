using System.Threading.Tasks;

namespace CoreTemplate.Managers
{
  public interface IEmailSender
  {
    Task SendEmailAsync(string email, string subject, string message);
  }
}