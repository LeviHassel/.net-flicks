using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.Managers.ViewModels.Manage
{
  public class AddPhoneNumberViewModel
  {
    [Required]
    [Phone]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }
  }
}