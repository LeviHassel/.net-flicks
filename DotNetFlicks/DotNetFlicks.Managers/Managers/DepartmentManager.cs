using AutoMapper;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Common.Models;
using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels.Department;
using DotNetFlicks.ViewModels.Shared;
using System.Collections.Generic;

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

        public DepartmentsViewModel GetAllByRequest(DataTableRequest request)
        {
            var dtos = _departmentAccessor.GetAllByRequest(request);
            var vms = Mapper.Map<List<DepartmentViewModel>>(dtos);

            vms.ForEach(x => x.PeopleCount = _departmentAccessor.GetRoleCount(x.Id));

            var filteredCount = _departmentAccessor.GetCount(request.Search);
            var totalCount = _departmentAccessor.GetCount();

            return new DepartmentsViewModel
            {
                Departments = vms,
                DataTable = new DataTableViewModel(request, filteredCount, totalCount)
            };
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
