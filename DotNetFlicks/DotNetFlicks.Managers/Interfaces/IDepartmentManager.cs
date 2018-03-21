using DotNetFlicks.ViewModels.Department;

namespace DotNetFlicks.Managers.Interfaces
{
    public interface IDepartmentManager
    {
        DepartmentViewModel Get(int? id);

        DepartmentsViewModel GetAll();

        DepartmentViewModel Save(DepartmentViewModel vm);

        DepartmentViewModel Delete(int id);
    }
}
