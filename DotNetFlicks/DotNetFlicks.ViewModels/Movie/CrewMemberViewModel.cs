using DotNetFlicks.ViewModels.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetFlicks.ViewModels.Movie
{
    public class CrewMemberViewModel : EntityViewModel
    {
        public int Index { get; set; }

        public bool IsDeleted { get; set; }

        public int PersonId { get; set; }

        public string PersonName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string Position { get; set; }
    }
}
