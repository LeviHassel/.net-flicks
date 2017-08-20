using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.Managers.ViewModels.Account
{
  public class ExternalLoginConfirmationViewModel
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}