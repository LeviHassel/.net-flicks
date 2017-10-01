using CoreTemplate.Managers.ViewModels.Manage;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IAccountManager
    {
        Task<string> Get2faUserId();

        Task<SignInResult> LoginWithPassword(string email, string password, bool rememberMe);

        Task<SignInResult> LoginWithRecoveryCode(string recoveryCode);

        Task SignOut();

        Task<string> GetPasswordResetToken(string email);

        Task<string> GetUserId(string email);

        Task<string> GetUserId(ClaimsPrincipal user);

        Task<ExternalLoginInfo> GetExternalLoginInfo();

        Task<bool> UserHasPassword(ClaimsPrincipal User);

        Task RemoveLogin(ClaimsPrincipal User, string loginProvider, string providerKey);

        Task AddLogin(ClaimsPrincipal User);

        Task<TwoFactorAuthenticationViewModel> GetTwoFactorAuthenticationViewModel(ClaimsPrincipal User);
    }
}
