using AutoMapper;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels.Department;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Managers.Managers
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
            vm.PeopleCount = id.HasValue ? _departmentAccessor.GetRoleCount(id.Value) : 0;

            return vm;
        }

        public DepartmentsViewModel GetAll()
        {
            var dtos = _departmentAccessor.GetAll();
            var vms = Mapper.Map<List<DepartmentViewModel>>(dtos);

            foreach (var vm in vms)
            {
                vm.PeopleCount = _departmentAccessor.GetRoleCount(vm.Id);
            }

            return new DepartmentsViewModel { Departments = vms.OrderBy(x => x.Name).ToList() };
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
