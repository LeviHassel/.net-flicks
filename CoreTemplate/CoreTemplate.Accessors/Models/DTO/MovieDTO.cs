using System.ComponentModel.DataAnnotations;
using CoreTemplate.Accessors.Models.DTO.Base;

namespace CoreTemplate.Accessors.Models.DTO
{
    public class MovieDTO : EntityDTO
    {
        public string Name { get; set; }

        public string Genre { get; set; }

        public string Director { get; set; }

        public int Runtime { get; set; }

        public int Year { get; set; }
    }
}
