using System.ComponentModel.DataAnnotations;
using CoreTemplate.Accessors.Models.EF.Base;

namespace CoreTemplate.Accessors.Models.EF
{
  public class MyTable : Entity
    {
    public int? Bar { get; set; }

    [Required]
    public string Foo { get; set; }
  }
}
