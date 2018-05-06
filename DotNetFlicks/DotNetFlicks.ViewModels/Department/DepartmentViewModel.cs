using DotNetFlicks.Common.Extensions;
using DotNetFlicks.ViewModels.Base;
using DotNetFlicks.ViewModels.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotNetFlicks.ViewModels.Department
{
    public class DepartmentViewModel : EntityViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public bool IsDirecting { get; set; }

        public int TmdbId { get; set; }

        public List<MovieRoleViewModel> People { get; set; }

        public string PeopleTooltip { get { return People != null ? People.Select(x => string.Format("{0} - {1}", x.PersonName, x.MovieName)).ToList().ToTooltipList() : ""; } }

        public string PeopleBulletedList { get { return People != null ? People.Select(x => string.Format("{0} - {1}", x.PersonName, x.MovieName)).ToList().ToTooltipList() : ""; } }
    }
}
