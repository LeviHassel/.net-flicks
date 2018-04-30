using System.Threading.Tasks;

namespace DotNetFlicks.Managers.Interfaces
{
    public interface IEmailManager
    {
        Task SendEmailAsync(string email, string subject, string message);

        Task SendEmailConfirmationAsync(string email, string link);
    }
}