using System.ComponentModel.DataAnnotations;
using CoreTemplate.Managers.ViewModels.Base;

namespace CoreTemplate.Managers.ViewModels.Home
{
    public class MovieViewModel : EntityViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public int Runtime { get; set; }

        [Required]
        public int Year { get; set; }
    }
}
