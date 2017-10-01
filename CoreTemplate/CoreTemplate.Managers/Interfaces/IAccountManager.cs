using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IAccountManager
    {
        Task<string> Get2faUserId();

        Task<SignInResult> Login(string email, string password, bool rememberMe);
    }
}
