using CoreTemplate.Accessors.Models.DTO.Base;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Models.DTO
{
    public class PersonDTO : EntityDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<MoviePersonDTO> Movies { get; set; }
    }
}
