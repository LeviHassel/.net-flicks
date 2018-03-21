using DotNetFlicks.Accessors.Identity;
using DotNetFlicks.Accessors.Models.DTO.Base;
using System;

namespace DotNetFlicks.Accessors.Models.DTO
{
    public class UserMovieDTO : EntityDTO
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int MovieId { get; set; }

        public MovieDTO Movie { get; set; }

        public DateTime? RentEndDate { get; set; }

        public DateTime? PurchaseDate { get; set; }
    }
}
