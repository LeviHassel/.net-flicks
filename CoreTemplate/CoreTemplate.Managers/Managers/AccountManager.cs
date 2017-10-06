using CoreTemplate.Accessors.Identity;
using CoreTemplate.Managers.Identity;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.Managers.ViewModels.Account;
using CoreTemplate.Managers.ViewModels.Manage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CoreTemplate.Managers.Managers
{
    public class AccountManager : IAccountManager
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private ILogger _logger;
        private UrlEncoder _urlEncoder;

        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public AccountManager(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            ILogger<AccountManager> logger,
            UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _urlEncoder = urlEncoder;
        }

        #region Sign In/Out
        public async Task<SignInResult> SignInWithPassword(string email, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        }

        public async Task SignInWithEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task<SignInResult> SignInWithRecoveryCode(string recoveryCode)
        {
            return await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
        #endregion

        #region Token Generation
        public async Task<string> GetEmailConfirmationToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GetPasswordResetToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return null;
            }

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
        #endregion

        #region Account Management
        public async Task<IndexViewModel> GetIndex(ClaimsPrincipal identityUser)
        {
            var user = await GetApplicationUser(identityUser);

            var vm = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed
            };

            return vm;
        }

        public async Task<IdentityResult> CreateUser(RegisterViewModel vm)
        {
            var user = _userManager.CreateUser(vm.Email);

            var result = await _userManager.CreateAsync(user, vm.Password);

            return result;
        }

        public async Task UpdateUser(ClaimsPrincipal identityUser, IndexViewModel vm)
        {
            var user = await GetApplicationUser(identityUser);

            var email = user.Email;
            if (vm.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, vm.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (vm.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, vm.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }
        }

        public async Task<IdentityResult> ChangePassword(ClaimsPrincipal identityUser, ChangePasswordViewModel vm)
        {
            var user = await GetApplicationUser(identityUser);

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, vm.OldPassword, vm.NewPassword);

            if (changePasswordResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                _logger.LogInformation("User changed their password successfully.");
            }

            return changePasswordResult;
        }

        public async Task<IdentityResult> SetPassword(ClaimsPrincipal identityUser, SetPasswordViewModel vm)
        {
            var user = await GetApplicationUser(identityUser);

            var addPasswordResult = await _userManager.AddPasswordAsync(user, vm.NewPassword);

            await _signInManager.SignInAsync(user, isPersistent: false);

            return addPasswordResult;
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordViewModel vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.Email);

            if (user == null)
            {
                return null;
            }

            var result = await _userManager.ResetPasswordAsync(user, vm.Code, vm.Password);

            return result;
        }

        public async Task<IdentityResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result;
        }
        #endregion

        #region Two-Factor Authentication
        public async Task<string> Get2faUserId()
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            return user.Id;
        }

        public async Task<SignInResult> LoginWith2fa(string authenticatorCode, bool rememberMe, bool rememberMachine)
        {
            return await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, rememberMachine);
        }

        public async Task<TwoFactorAuthenticationViewModel> GetTwoFactorAuthentication(ClaimsPrincipal identityUser)
        {
            var user = await GetApplicationUser(identityUser);

            var vm = new TwoFactorAuthenticationViewModel
            {
                HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
                Is2faEnabled = user.TwoFactorEnabled,
                RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user),
            };

            return vm;
        }

        public async Task<EnableAuthenticatorViewModel> GetEnableAuthenticator(ClaimsPrincipal identityUser)
        {
            var user = await GetApplicationUser(identityUser);

            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);

            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            var vm = new EnableAuthenticatorViewModel
            {
                SharedKey = FormatKey(unformattedKey),
                AuthenticatorUri = GenerateQrCodeUri(user.Email, unformattedKey)
            };

            return vm;
        }

        public async Task<bool> EnableAuthenticator(ClaimsPrincipal identityUser, EnableAuthenticatorViewModel vm)
        {
            var user = await GetApplicationUser(identityUser);

            // Strip spaces and hypens
            var verificationCode = vm.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (is2faTokenValid)
            {
                var result = await _userManager.SetTwoFactorEnabledAsync(user, true);

                _logger.LogInformation("User with ID {UserId} has enabled 2FA with an authenticator app.", user.Id);

                return result.Succeeded;
            }

            return false;
        }

        public async Task ResetAuthenticator(ClaimsPrincipal identityUser)
        {
            var user = await GetApplicationUser(identityUser);

            await _userManager.SetTwoFactorEnabledAsync(user, false);

            await _userManager.ResetAuthenticatorKeyAsync(user);

            _logger.LogInformation("User with id '{UserId}' has reset their authentication app key.", user.Id);
        }

        public async Task<GenerateRecoveryCodesViewModel> GetGenerateRecoveryCodes(ClaimsPrincipal identityUser)
        {
            var user = await GetApplicationUser(identityUser);

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException($"Cannot generate recovery codes for user with ID '{user.Id}' as they do not have 2FA enabled.");
            }

            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);

            var vm = new GenerateRecoveryCodesViewModel { RecoveryCodes = recoveryCodes.ToArray() };

            _logger.LogInformation("User with ID {UserId} has generated new 2FA recovery codes.", user.Id);

            return vm;
        }

        public async Task Disable2faWarning(ClaimsPrincipal identityUser)
        {
            var user = await GetApplicationUser(identityUser);

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{user.Id}'.");
            }
        }

        public async Task Disable2fa(ClaimsPrincipal identityUser)
        {
            var user = await GetApplicationUser(identityUser);

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);

            if (!disable2faResult.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{user.Id}'.");
            }

            _logger.LogInformation("User with ID {UserId} has disabled 2fa.", user.Id);
        }
        #endregion

        #region External Authentication
        public async Task<ExternalLoginInfo> GetExternalLoginInfo()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInResult> LoginExternal(ExternalLoginInfo info)
        {
            return await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        }

        public async Task<ExternalLoginsViewModel> GetExternalLogins(ClaimsPrincipal identityUser)
        {
            var user = await GetApplicationUser(identityUser);

            var vm = new ExternalLoginsViewModel { CurrentLogins = await _userManager.GetLoginsAsync(user) };

            vm.OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                .Where(auth => vm.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();

            vm.ShowRemoveButton = await _userManager.HasPasswordAsync(user) || vm.CurrentLogins.Count > 1;

            return vm;
        }

        public async Task AddExternalLogin(ClaimsPrincipal identityUser)
        {
            var user = await GetApplicationUser(identityUser);

            var info = await GetExternalLoginInfo();

            if (info == null)
            {
                throw new ApplicationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }

            var result = await _userManager.AddLoginAsync(user, info);

            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'.");
            }
        }

        public async Task RemoveExternalLogin(ClaimsPrincipal identityUser, string loginProvider, string providerKey)
        {
            var user = await GetApplicationUser(identityUser);

            var result = await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);

            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred removing external login for user with ID '{user.Id}'.");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task<IdentityResult> CreateUserExternal(ExternalLoginViewModel vm)
        {
            // Get the information about the user from the external login provider
            var info = await GetExternalLoginInfo();

            if (info == null)
            {
                throw new ApplicationException("Error loading external login information during confirmation.");
            }

            var user = _userManager.CreateUser(vm.Email);

            var createUserResult = await _userManager.CreateAsync(user);

            if (createUserResult.Succeeded)
            {
                var addLoginResult = await _userManager.AddLoginAsync(user, info);

                if (createUserResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                }
            }

            return createUserResult;
        }

        public async Task<AuthenticationProperties> ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, ClaimsPrincipal identityUser = null)
        {
            string userId = null;

            if (identityUser != null)
            {
                var user = await GetApplicationUser(identityUser);

                userId = user.Id;
            }

            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);
        }
        #endregion

        #region Account Utilities
        public async Task<string> GetUserId(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user.");
            }

            return user.Id;
        }

        public async Task<bool> UserHasPassword(ClaimsPrincipal identityUser)
        {
            var user = await GetApplicationUser(identityUser);

            return await _userManager.HasPasswordAsync(user);
        }
        #endregion

        #region Helpers
        private async Task<ApplicationUser> GetApplicationUser(ClaimsPrincipal identityUser)
        {
            var user = await _userManager.GetUserAsync(identityUser);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{user.Id}'.");
            }

            return user;
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenicatorUriFormat,
                _urlEncoder.Encode("Test"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }
        #endregion
    }
}
