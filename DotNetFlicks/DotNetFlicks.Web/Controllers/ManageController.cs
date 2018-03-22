using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels.Manage;
using DotNetFlicks.Web.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetFlicks.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly IEmailManager _emailManager;

        public ManageController(
          IAccountManager accountManager,
          IEmailManager emailManager)
        {
            _accountManager = accountManager;
            _emailManager = emailManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        #region Account Management
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = await _accountManager.GetIndex(User);

            vm.StatusMessage = StatusMessage;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _accountManager.UpdateUser(User, vm);

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var code = await _accountManager.GetEmailConfirmationToken(vm.Email);

            var userId = await _accountManager.GetUserId(vm.Email);

            var callbackUrl = Url.EmailConfirmationLink(userId, code, Request.Scheme);

            await _emailManager.SendEmailConfirmationAsync(vm.Email, callbackUrl);

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var hasPassword = await _accountManager.UserHasPassword(User);

            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var vm = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var result = await _accountManager.ChangePassword(User, vm);

            if (!result.Succeeded)
            {
                AddErrors(result);
                return View(vm);
            }

            StatusMessage = "Your password has been changed.";
            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var hasPassword = await _accountManager.UserHasPassword(User);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var vm = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var result = await _accountManager.SetPassword(User, vm);
            
            if (!result.Succeeded)
            {
                AddErrors(result);
                return View(vm);
            }

            StatusMessage = "Your password has been set.";
            return RedirectToAction(nameof(SetPassword));
        }
        #endregion

        #region Two-Factor Authentication
        [HttpGet]
        public async Task<IActionResult> TwoFactorAuthentication()
        {
            var vm = await _accountManager.GetTwoFactorAuthentication(User);

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Disable2faWarning()
        {
            await _accountManager.Disable2faWarning(User);

            return View(nameof(Disable2fa));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable2fa()
        {
            await _accountManager.Disable2fa(User);

            return RedirectToAction(nameof(TwoFactorAuthentication));
        }
        
        [HttpGet]
        public async Task<IActionResult> EnableAuthenticator()
        {
            var vm = await _accountManager.GetEnableAuthenticator(User);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableAuthenticator(EnableAuthenticatorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var successfullyEnabled = await _accountManager.EnableAuthenticator(User, vm);

            if (successfullyEnabled)
            {
                return RedirectToAction(nameof(GenerateRecoveryCodes));
            }

            ModelState.AddModelError("vm.Code", "Verification code is invalid.");

            return View(vm);
        }

        [HttpGet]
        public IActionResult ResetAuthenticatorWarning()
        {
            return View(nameof(ResetAuthenticator));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetAuthenticator()
        {
            await _accountManager.ResetAuthenticator(User);

            return RedirectToAction(nameof(EnableAuthenticator));
        }

        [HttpGet]
        public async Task<IActionResult> GenerateRecoveryCodes()
        {
            var vm = await _accountManager.GetGenerateRecoveryCodes(User);

            return View(vm);
        }
        #endregion

        #region External Authentication
        [HttpGet]
        public async Task<IActionResult> ExternalLogins()
        {
            var vm = await _accountManager.GetExternalLogins(User);

            vm.StatusMessage = StatusMessage;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkLogin(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action(nameof(LinkLoginCallback));

            var properties = await _accountManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, User);

            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        public async Task<IActionResult> LinkLoginCallback()
        {
            await _accountManager.AddExternalLogin(User);

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            StatusMessage = "The external login was added.";
            return RedirectToAction(nameof(ExternalLogins));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveExternalLogin(RemoveLoginViewModel vm)
        {
            await _accountManager.RemoveExternalLogin(User, vm.LoginProvider, vm.ProviderKey);

            StatusMessage = "The external login was removed.";
            return RedirectToAction(nameof(ExternalLogins));
        }
        #endregion

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}
