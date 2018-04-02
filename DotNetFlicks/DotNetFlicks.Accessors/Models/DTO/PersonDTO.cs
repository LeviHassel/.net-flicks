using DotNetFlicks.Accessors.Models.DTO.Base;
using System;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Models.DTO
{
    public class PersonDTO : EntityDTO
    {
        public string Name { get; set; }

        public string Biography { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<CastMemberDTO> CastRoles { get; set; }

        public ICollection<CrewMemberDTO> CrewRoles { get; set; }
    }
}
