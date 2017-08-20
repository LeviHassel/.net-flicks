using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.Managers.ViewModels.Account
{
  public class VerifyCodeViewModel
  {
    [Required]
    public string Provider { get; set; }

    [Required]
    public string Code { get; set; }

    public string ReturnUrl { get; set; }

    [Display(Name = "Remember this browser?")]
    public bool RememberBrowser { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }
}