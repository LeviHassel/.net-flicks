using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.ViewModels.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
