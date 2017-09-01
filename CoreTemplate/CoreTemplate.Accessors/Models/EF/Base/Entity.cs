using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.Accessors.Models.EF.Base
{
    public class Entity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
