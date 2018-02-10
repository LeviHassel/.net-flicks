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

        public string ImageUrl { get; set; }
    }
}
