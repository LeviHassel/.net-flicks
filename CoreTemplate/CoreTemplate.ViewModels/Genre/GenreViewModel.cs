using CoreTemplate.ViewModels.Base;
using CoreTemplate.ViewModels.Movie;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.ViewModels.Genre
{
    public class GenreViewModel : EntityViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public List<MovieViewModel> Movies { get; set; }

        public int MoviesCount { get; set; }

        [Display(Name = "Movies")]
        public string MoviesTooltip { get; set; }
    }
}
