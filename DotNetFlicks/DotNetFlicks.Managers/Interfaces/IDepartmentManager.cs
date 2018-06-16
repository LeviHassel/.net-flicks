using DotNetFlicks.Common.Models;
using DotNetFlicks.ViewModels.Department;

namespace DotNetFlicks.Managers.Interfaces
{
    public interface IDepartmentManager
    {
        DepartmentViewModel Get(int? id);

        DepartmentsViewModel GetAllByRequest(DataTableRequest query);

        DepartmentViewModel Save(DepartmentViewModel vm);

        DepartmentViewModel Delete(int id);
    }
}
