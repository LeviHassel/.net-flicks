using DotNetFlicks.Common.Helpers;
using DotNetFlicks.ViewModels.Base;
using DotNetFlicks.ViewModels.Movie;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotNetFlicks.ViewModels.Genre
{
    public class GenreViewModel : EntityViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public List<MovieViewModel> Movies { get; set; }

        public string MoviesTooltip { get { return Movies != null ? ListHelper.GetTooltipList(Movies.Select(x => x.Name).ToList()) : ""; } }

        public string MoviesBulletedList { get { return Movies != null ? ListHelper.GetBulletedList(Movies.Select(x => x.Name).ToList()) : ""; } }
    }
}
