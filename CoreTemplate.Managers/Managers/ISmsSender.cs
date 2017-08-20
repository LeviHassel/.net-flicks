using System.Threading.Tasks;

namespace CoreTemplate.Managers
{
  public interface ISmsSender
  {
    Task SendSmsAsync(string number, string message);
  }
}