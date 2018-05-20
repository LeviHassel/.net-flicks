using DotNetFlicks.Accessors.Models.DTO.Base;
using System;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Models.DTO
{
    public class MovieDTO : EntityDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public TimeSpan Runtime { get; set; }

        public string ImageUrl { get; set; }

        public string TrailerUrl { get; set; }

        public decimal PurchaseCost { get; set; }

        public decimal RentCost { get; set; }

        public ICollection<MovieGenreDTO> Genres { get; set; }

        public ICollection<CastMemberDTO> Cast { get; set; }

        public ICollection<CrewMemberDTO> Crew { get; set; }
    }
}
