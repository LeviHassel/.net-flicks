using CoreTemplate.Accessors.Models.DTO.Base;
using System;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Models.DTO
{
    public class PersonDTO : EntityDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<CastMemberDTO> CastRoles { get; set; }

        public ICollection<CrewMemberDTO> CrewRoles { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
    }
}
