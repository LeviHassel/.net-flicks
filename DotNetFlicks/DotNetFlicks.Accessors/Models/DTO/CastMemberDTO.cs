using DotNetFlicks.Accessors.Models.DTO.Base;
using System;

namespace DotNetFlicks.Accessors.Models.DTO
{
    public class CastMemberDTO : EntityDTO
    {
        public int MovieId { get; set; }

        public MovieDTO Movie { get; set; }

        public int PersonId { get; set; }

        public PersonDTO Person { get; set; }

        public string Role { get; set; }

        public int Order { get; set; }
    }
}
