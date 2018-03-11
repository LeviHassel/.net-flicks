using CoreTemplate.Common.Helpers;
using CoreTemplate.ViewModels.Base;
using CoreTemplate.ViewModels.Movie;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CoreTemplate.ViewModels.Genre
{
    public class GenreViewModel : EntityViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public List<MovieViewModel> Movies { get; set; }

        public string MoviesTooltip { get { return ListHelper.GetTooltipList(Movies.Select(x => x.Name).ToList()); } }

        public string MoviesBulletedList { get { return ListHelper.GetBulletedList(Movies.Select(x => x.Name).ToList()); } }
    }
}
