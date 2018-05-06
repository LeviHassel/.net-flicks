using DotNetFlicks.Accessors.Models.DTO;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface IDepartmentAccessor
    {
        DepartmentDTO Get(int id);

        List<DepartmentDTO> GetAll();

        List<DepartmentDTO> GetByName(string query);

        DepartmentDTO Save(DepartmentDTO dto);

        DepartmentDTO Delete(int id);
    }
}
