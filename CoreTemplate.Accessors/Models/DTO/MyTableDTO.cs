using System.ComponentModel.DataAnnotations;
using CoreTemplate.Accessors.Models.DTO.Base;

namespace CoreTemplate.Accessors.Models.DTO
{
  public class MyTableDTO : EntityDTO
    {
    public int? Bar { get; set; }

    [Required]
    public string Foo { get; set; }
  }
}
