using CoreTemplate.Accessors.Models.DTO.Base;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Models.DTO
{
    public class GenreDTO : EntityDTO
    {
        public string Name { get; set; }

        public ICollection<MovieGenreDTO> Movies { get; set; }
    }
}
