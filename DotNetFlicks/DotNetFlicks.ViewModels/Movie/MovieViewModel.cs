using DotNetFlicks.Common.Utilities;
using DotNetFlicks.ViewModels.Base;
using DotNetFlicks.ViewModels.Genre;
using DotNetFlicks.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotNetFlicks.ViewModels.Movie
{
    public class MovieViewModel : EntityViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

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

        public DateTime? RentEndDate { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public List<GenreViewModel> Genres { get; set; }

        public List<MovieRoleViewModel> Cast { get; set; }

        public List<MovieRoleViewModel> Crew { get; set; }

        public string GenresTooltip { get { return Genres != null ? Genres.Select(x => x.Name).OrderBy(y => y).ToList().ToTooltipList() : ""; } }

        public bool CurrentlyRented { get { return RentEndDate.HasValue && RentEndDate.Value >= DateTime.UtcNow; } }

        public bool Purchased { get { return PurchaseDate.HasValue; } }

        public string PurchaseDateString
        {
            get
            {
                if (RentEndDate.HasValue)
                {
                    return ((DateTimeOffset)RentEndDate.Value.AddDays(-2)).ToUnixTimeSeconds().ToString();
                }
                else if (PurchaseDate.HasValue)
                {
                    return ((DateTimeOffset)PurchaseDate.Value).ToUnixTimeSeconds().ToString();
                }
                else
                {
                    return string.Empty;
                }

            }
        }
    }
}
