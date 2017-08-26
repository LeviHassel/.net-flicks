using System.Threading.Tasks;

namespace CoreTemplate.Managers
{
  public interface IEmailManager
  {
    Task SendEmailAsync(string email, string subject, string message);
  }
}