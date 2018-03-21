using DotNetFlicks.Accessors.Models.EF.Base;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Models.EF
{
    public class Genre : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<MovieGenre> Movies { get; set; }
    }
}
