using System.ComponentModel.DataAnnotations;
using CoreTemplate.Managers.ViewModels.Base;

namespace CoreTemplate.Managers.ViewModels.Home
{
  public class MyTableViewModel : EntityViewModel
  {
    [RegularExpression("(-)?([0-9]+)", ErrorMessage = "Please enter an integer value.")]
    public int? Bar { get; set; }

    [Required]
    public string Foo { get; set; }

    [Display(Name = "Bar + Foo")]
    public string BarPlusFoo
    {
      get
      {
        return string.Format("{0} {1}", Bar, Foo).Trim();
      }
    }
  }
}
