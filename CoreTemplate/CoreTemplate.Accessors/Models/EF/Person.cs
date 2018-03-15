using CoreTemplate.Accessors.Models.EF.Base;
using System;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Models.EF
{
    public class Person : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<CastMember> CastRoles { get; set; }

        public virtual ICollection<CrewMember> CrewRoles { get; set; }
    }
}
