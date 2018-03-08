using CoreTemplate.ViewModels.Department;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IDepartmentManager
    {
        DepartmentViewModel Get(int? id);

        DepartmentsViewModel GetAll();

        DepartmentViewModel Save(DepartmentViewModel vm);

        DepartmentViewModel Delete(int id);
    }
}
