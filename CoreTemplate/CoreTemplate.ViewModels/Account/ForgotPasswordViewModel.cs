using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
