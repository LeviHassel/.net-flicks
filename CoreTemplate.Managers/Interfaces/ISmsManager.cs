using System.Threading.Tasks;

namespace CoreTemplate.Managers
{
    public interface ISmsManager
    {
        Task SendSmsAsync(string number, string message);
    }
}