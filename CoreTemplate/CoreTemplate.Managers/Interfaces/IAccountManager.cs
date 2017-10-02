using CoreTemplate.Managers.ViewModels.Account;
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

        Task<string> GetUserId(ClaimsPrincipal identityUser);

        Task<ExternalLoginInfo> GetExternalLoginInfo();

        Task<bool> UserHasPassword(ClaimsPrincipal identityUser);

        Task RemoveLogin(ClaimsPrincipal identityUser, string loginProvider, string providerKey);

        Task AddLogin(ClaimsPrincipal identityUser);

        Task ResetAuthenticator(ClaimsPrincipal identityUser);

        Task Disable2faWarning(ClaimsPrincipal identityUser);

        Task Disable2fa(ClaimsPrincipal identityUser);

        Task<bool> EnableAuthenticator(ClaimsPrincipal identityUser, EnableAuthenticatorViewModel model);

        Task UpdateUser(ClaimsPrincipal identityUser, IndexViewModel model);

        Task<IdentityResult> CreateUserExternal(ExternalLoginViewModel model);

        Task<IdentityResult> ChangePassword(ClaimsPrincipal identityUser, ChangePasswordViewModel model);

        Task<IdentityResult> SetPassword(ClaimsPrincipal identityUser, SetPasswordViewModel model);

        Task<IdentityResult> ResetPassword(ResetPasswordViewModel model);

        Task<IdentityResult> ConfirmEmail(string userId, string code);

        Task<GenerateRecoveryCodesViewModel> GetGenerateRecoveryCodesViewModel(ClaimsPrincipal identityUser);

        Task<EnableAuthenticatorViewModel> GetEnableAuthenticatorViewModel(ClaimsPrincipal identityUser);

        Task<ExternalLoginsViewModel> GetExternalLoginsViewModel(ClaimsPrincipal identityUser);

        Task<IndexViewModel> GetIndexViewModel(ClaimsPrincipal identityUser);

        Task<TwoFactorAuthenticationViewModel> GetTwoFactorAuthenticationViewModel(ClaimsPrincipal identityUser);
    }
}
