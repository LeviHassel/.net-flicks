using CoreTemplate.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.ViewModels.Department
{
    public class DepartmentViewModel : EntityViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public int PeopleCount { get; set; }

        [Display(Name = "People")]
        public string PeopleTooltip { get; set; }
    }
}
