using CoreTemplate.ViewModels.Base;
using CoreTemplate.ViewModels.Genre;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.ViewModels.Movie
{
    public class MovieViewModel : EntityViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Runtime { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Purchase Cost")]
        public int PurchaseCost { get; set; }

        [Required]
        [Display(Name = "Rent Cost")]
        public int RentCost { get; set; }

        [Display(Name = "Genres")]
        public MultiSelectList GenresSelectList { get; set; }

        public List<int> GenreIds { get; set; }

        public List<MoviePersonViewModel> People { get; set; }
    }
}
