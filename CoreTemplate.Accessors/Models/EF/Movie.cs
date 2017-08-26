using System.ComponentModel.DataAnnotations;
using CoreTemplate.Accessors.Models.EF.Base;

namespace CoreTemplate.Accessors.Models.EF
{
    public class Movie : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string Director { get; set; }

        public int Runtime { get; set; }

        public int Year { get; set; }
    }
}
