using CoreTemplate.Accessors.Models.DTO.Base;

namespace CoreTemplate.Accessors.Models.DTO
{
    public class DepartmentDTO : EntityDTO
    {
        public string Name { get; set; }

        public bool IsDirecting { get; set; }
    }
}
