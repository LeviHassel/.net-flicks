using DotNetFlicks.Common.Utilities;
using DotNetFlicks.ViewModels.Base;
using DotNetFlicks.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotNetFlicks.ViewModels.Person
{
    public class PersonViewModel : EntityViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string Biography { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Death Date")]
        public DateTime? DeathDate { get; set; }

        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        public List<MovieRoleViewModel> Roles { get; set; }

        public string RolesTooltip { get { return Roles != null ? Roles.Select(x => string.Format("{0} ({1})", x.MovieName, x.Role)).ToList().ToTooltipList() : ""; } }

        public string RolesBulletedList { get { return Roles != null ? Roles.Select(x => string.Format("{0} ({1})", x.MovieName, x.Role)).ToList().ToTooltipList() : ""; } }

        public string KnownFor { get { return Roles != null && Roles.Any() ? Roles.GroupBy(x => x.Category).OrderByDescending(x => x.Count()).First().Key : ""; } }

        public int Age { get { return DateUtility.GetAge(BirthDate, DeathDate); } }
    }
}
