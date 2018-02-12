using CoreTemplate.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.ViewModels.Movie
{
    public class MoviePersonViewModel : EntityViewModel
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public int JobId { get; set; }
    }
}
