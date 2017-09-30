using AutoMapper;
using CoreTemplate.Managers.Identity;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.Managers.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        public async void Get2faUser()
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }
        }
    }
}
