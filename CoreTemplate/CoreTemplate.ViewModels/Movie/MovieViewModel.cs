using CoreTemplate.Common.Helpers;
using CoreTemplate.ViewModels.Base;
using CoreTemplate.ViewModels.Genre;
using CoreTemplate.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CoreTemplate.ViewModels.Movie
{
    public class MovieViewModel : EntityViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:%h}h {0:%m}m")]
        public TimeSpan Runtime { get; set; }

        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Url]
        [Display(Name = "Trailer URL")]
        public string TrailerUrl { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Purchase")]
        public decimal PurchaseCost { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Rent")]
        public decimal RentCost { get; set; }

        public List<GenreViewModel> Genres { get; set; }

        public List<MovieRoleViewModel> Cast { get; set; }

        public List<MovieRoleViewModel> Crew { get; set; }

        public string GenresTooltip { get { return Genres != null ? ListHelper.GetTooltipList(Genres.Select(x => x.Name).OrderBy(y => y).ToList()) : ""; } }
    }
}
