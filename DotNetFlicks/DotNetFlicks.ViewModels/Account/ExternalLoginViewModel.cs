using System.ComponentModel.DataAnnotations;

namespace DotNetFlicks.ViewModels.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
