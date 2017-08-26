using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.Managers.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}