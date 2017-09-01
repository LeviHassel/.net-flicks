using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.Managers.ViewModels.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
