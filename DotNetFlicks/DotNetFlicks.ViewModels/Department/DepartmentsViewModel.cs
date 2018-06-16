using DotNetFlicks.ViewModels.Shared;
using System.Collections.Generic;

namespace DotNetFlicks.ViewModels.Department
{
    public class DepartmentsViewModel
    {
        public List<DepartmentViewModel> Departments { get; set; }

        public DataTableViewModel DataTable { get; set; }
    }
}
