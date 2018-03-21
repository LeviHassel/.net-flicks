using DotNetFlicks.Accessors.Identity;
using DotNetFlicks.Accessors.Models.EF.Base;
using System;

namespace DotNetFlicks.Accessors.Models.EF
{
    public class UserMovie : Entity
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public DateTime? RentEndDate { get; set; }

        public DateTime? PurchaseDate { get; set; }
    }
}
