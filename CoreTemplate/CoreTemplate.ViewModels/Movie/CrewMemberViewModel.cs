using CoreTemplate.ViewModels.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.ViewModels.Movie
{
    public class CrewMemberViewModel : EntityViewModel
    {
        public int Index { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string Position { get; set; }

        public SelectList People { get; set; }

        public SelectList Departments { get; set; }
    }
}
