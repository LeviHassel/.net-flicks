using CoreTemplate.Common.Helpers;
using CoreTemplate.ViewModels.Base;
using CoreTemplate.ViewModels.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CoreTemplate.ViewModels.Department
{
    public class DepartmentViewModel : EntityViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public List<MovieRoleViewModel> People { get; set; }

        public string PeopleTooltip { get { return People != null ? ListHelper.GetTooltipList(People.Select(x => string.Format("{0} - {1}", x.PersonName, x.MovieName)).ToList()) : ""; } }

        public string PeopleBulletedList { get { return People != null ? ListHelper.GetBulletedList(People.Select(x => string.Format("{0} - {1}", x.PersonName, x.MovieName)).ToList()) : ""; } }
    }
}
