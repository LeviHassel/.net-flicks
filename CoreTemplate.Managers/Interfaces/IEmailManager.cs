using System.Threading.Tasks;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IEmailManager
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}