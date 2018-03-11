using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Department;
using System.Collections.Generic;

namespace CoreTemplate.Managers.Managers
{
    public class DepartmentManager : IDepartmentManager
    {
        private IDepartmentAccessor _departmentAccessor;

        public DepartmentManager(IDepartmentAccessor departmentAccessor)
        {
            _departmentAccessor = departmentAccessor;
        }

        public DepartmentViewModel Get(int? id)
        {
            var dto = id.HasValue ? _departmentAccessor.Get(id.Value) : new DepartmentDTO();
            var vm = Mapper.Map<DepartmentViewModel>(dto);
            return vm;
        }

        public DepartmentsViewModel GetAll()
        {
            var dtos = _departmentAccessor.GetAll();
            var vms = Mapper.Map<List<DepartmentViewModel>>(dtos);
            return new DepartmentsViewModel { Departments = vms };
        }

        public DepartmentViewModel Save(DepartmentViewModel vm)
        {
            var dto = Mapper.Map<DepartmentDTO>(vm);
            dto = _departmentAccessor.Save(dto);
            vm = Mapper.Map<DepartmentViewModel>(dto);
            return vm;
        }

        public DepartmentViewModel Delete(int id)
        {
            var dto = _departmentAccessor.Delete(id);
            var vm = Mapper.Map<DepartmentViewModel>(dto);
            return vm;
        }
    }
}
