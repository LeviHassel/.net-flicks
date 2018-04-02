using DotNetFlicks.Accessors.Models.EF.Base;
using System;

namespace DotNetFlicks.Accessors.Models.EF
{
    public class CastMember : Entity
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public string Role { get; set; }

        public int Order { get; set; }
    }
}
