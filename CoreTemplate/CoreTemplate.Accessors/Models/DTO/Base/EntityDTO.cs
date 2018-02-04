using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.Accessors.Models.DTO.Base
{
    public abstract class EntityDTO
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
