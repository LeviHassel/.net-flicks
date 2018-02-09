using CoreTemplate.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.ViewModels.Genre
{
    public class GenreViewModel : EntityViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
