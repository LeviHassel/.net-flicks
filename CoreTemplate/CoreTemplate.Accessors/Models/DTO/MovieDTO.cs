using CoreTemplate.Accessors.Models.DTO.Base;
using System;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Models.DTO
{
    public class MovieDTO : EntityDTO
    {
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Runtime { get; set; }

        public string ImageUrl { get; set; }

        public int PurchaseCost { get; set; }

        public int RentCost { get; set; }

        public ICollection<MovieGenreDTO> Genres { get; set; }

        public ICollection<MoviePersonDTO> People { get; set; }
    }
}
