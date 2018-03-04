using CoreTemplate.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.ViewModels.Person
{
    public class PersonViewModel : EntityViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        public int MoviesCount { get; set; }

        [Display(Name = "Movies")]
        public string MoviesTooltip { get; set; }
    }
}
