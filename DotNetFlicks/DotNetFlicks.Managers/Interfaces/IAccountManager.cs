using DotNetFlicks.Accessors.Identity;
using DotNetFlicks.ViewModels.Account;
using DotNetFlicks.ViewModels.Manage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetFlicks.Managers.Interfaces
{
    public interface IAccountManager
    {
        Task<SignInResult> SignInWithPassword(string email, string password, bool rememberMe);

        Task SignInWithEmail(string email);

        Task<SignInResult> SignInWithRecoveryCode(string recoveryCode);

        Task SignOut();

        Task<string> GetEmailConfirmationToken(string email);

        Task<string> GetPasswordResetToken(string email);

        Task<IndexViewModel> GetIndex(ClaimsPrincipal identityUser);

        Task<IdentityResult> CreateUser(RegisterViewModel vm);

        Task UpdateUser(ClaimsPrincipal identityUser, IndexViewModel vm);

        Task<IdentityResult> ChangePassword(ClaimsPrincipal identityUser, ChangePasswordViewModel vm);

        Task<IdentityResult> SetPassword(ClaimsPrincipal identityUser, SetPasswordViewModel vm);

        Task<IdentityResult> ResetPassword(ResetPasswordViewModel vm);

        Task<IdentityResult> ConfirmEmail(string userId, string code);

        Task<string> Get2faUserId();

        Task<SignInResult> LoginWith2fa(string authenticatorCode, bool rememberMe, bool rememberMachine);

        Task<TwoFactorAuthenticationViewModel> GetTwoFactorAuthentication(ClaimsPrincipal identityUser);

        Task<EnableAuthenticatorViewModel> GetEnableAuthenticator(ClaimsPrincipal identityUser);

        Task<bool> EnableAuthenticator(ClaimsPrincipal identityUser, EnableAuthenticatorViewModel vm);

        Task ResetAuthenticator(ClaimsPrincipal identityUser);

        Task<GenerateRecoveryCodesViewModel> GetGenerateRecoveryCodes(ClaimsPrincipal identityUser);

        Task Disable2faWarning(ClaimsPrincipal identityUser);

        Task Disable2fa(ClaimsPrincipal identityUser);

        Task<ExternalLoginInfo> GetExternalLoginInfo();

        Task<SignInResult> LoginExternal(ExternalLoginInfo info);

        Task<ExternalLoginsViewModel> GetExternalLogins(ClaimsPrincipal identityUser);

        Task AddExternalLogin(ClaimsPrincipal identityUser);

        Task RemoveExternalLogin(ClaimsPrincipal identityUser, string loginProvider, string providerKey);

        Task<IdentityResult> CreateUserExternal(ExternalLoginViewModel vm);

        Task<AuthenticationProperties> ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, ClaimsPrincipal identityUser = null);

        Task<ApplicationUser> GetApplicationUser(ClaimsPrincipal identityUser);

        Task<string> GetUserId(string email);

        Task<bool> UserHasPassword(ClaimsPrincipal identityUser);
    }
}
