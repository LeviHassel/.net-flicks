using CoreTemplate.Accessors.Models.EF.Base;
using System;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Models.EF
{
    public class Movie : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public TimeSpan Runtime { get; set; }

        public string ImageUrl { get; set; }

        public string TrailerUrl { get; set; }

        public decimal PurchaseCost { get; set; }

        public decimal RentCost { get; set; }

        public virtual ICollection<MovieGenre> Genres { get; set; }

        public virtual ICollection<CastMember> Cast { get; set; }

        public virtual ICollection<CrewMember> Crew { get; set; }
    }
}
