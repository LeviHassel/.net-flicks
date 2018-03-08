using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface IDepartmentAccessor
    {
        DepartmentDTO Get(int id);

        List<DepartmentDTO> GetAll();

        DepartmentDTO Save(DepartmentDTO dto);

        DepartmentDTO Delete(int id);
    }
}
