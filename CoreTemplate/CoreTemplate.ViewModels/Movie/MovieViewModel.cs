using CoreTemplate.ViewModels.Base;
using CoreTemplate.ViewModels.Genre;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.ViewModels.Movie
{
    public class MovieViewModel : EntityViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public TimeSpan Runtime { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Purchase")]
        public int PurchaseCost { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Rent")]
        public int RentCost { get; set; }

        public MultiSelectList GenresSelectList { get; set; }

        [Display(Name = "Genres")]
        public List<int> GenreIds { get; set; }

        public List<MoviePersonViewModel> People { get; set; }

        public int GenresCount { get; set; }

        [Display(Name = "Genres")]
        public string GenresTooltip { get; set; }

        public int PeopleCount { get; set; }

        [Display(Name = "Crew")]
        public string PeopleTooltip { get; set; }
    }
}
