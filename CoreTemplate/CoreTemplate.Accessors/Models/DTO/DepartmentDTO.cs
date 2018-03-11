using CoreTemplate.Accessors.Models.DTO.Base;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Models.DTO
{
    public class DepartmentDTO : EntityDTO
    {
        public string Name { get; set; }

        public bool IsDirecting { get; set; }

        public ICollection<CrewMemberDTO> Roles { get; set; }
    }
}
