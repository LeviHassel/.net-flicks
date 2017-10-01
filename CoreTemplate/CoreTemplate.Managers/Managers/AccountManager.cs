using CoreTemplate.Accessors.Identity;
using CoreTemplate.Managers.Identity;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.Managers.ViewModels.Manage;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreTemplate.Managers.Managers
{
    public class AccountManager : IAccountManager
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private IEmailManager _emailManager;
        private ILogger _logger;

        public AccountManager(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IEmailManager emailSender,
            ILogger<AccountManager> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailManager = emailSender;
            _logger = logger;
        }

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

        public async Task<SignInResult> LoginWithPassword(string email, string password, bool rememberMe)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        }

        public async Task<SignInResult> LoginWithRecoveryCode(string recoveryCode)
        {
            return await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
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

        public async Task<string> GetUserId(string email)
        {
            var user =  await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user.");
            }

            return user.Id;
        }

        public async Task<string> GetUserId(ClaimsPrincipal user)
        {
            var applicationUser = await _userManager.GetUserAsync(user);

            if (applicationUser == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(user)}'.");
            }

            return applicationUser.Id;
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfo()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<bool> UserHasPassword(ClaimsPrincipal User)
        {
            var user = await GetApplicationUser(User);

            return await _userManager.HasPasswordAsync(user);
        }

        public async Task RemoveLogin(ClaimsPrincipal User, string loginProvider, string providerKey)
        {
            var user = await GetApplicationUser(User);

            var result = await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);

            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred removing external login for user with ID '{user.Id}'.");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task AddLogin(ClaimsPrincipal User)
        {
            var user = await GetApplicationUser(User);

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

        public async Task<TwoFactorAuthenticationViewModel> GetTwoFactorAuthenticationViewModel(ClaimsPrincipal User)
        {
            var user = await GetApplicationUser(User);

            var model = new TwoFactorAuthenticationViewModel
            {
                HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
                Is2faEnabled = user.TwoFactorEnabled,
                RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user),
            };

            return model;
        }

        #region Private Methods
        private async Task<ApplicationUser> GetApplicationUser(ClaimsPrincipal user)
        {
            var applicationUser = await _userManager.GetUserAsync(user);

            if (applicationUser == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(user)}'.");
            }

            return applicationUser;
        }
        #endregion
    }
}
