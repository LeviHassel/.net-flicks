using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Common.Helpers;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Department;
using CoreTemplate.ViewModels.Shared;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Managers.Managers
{
    public class DepartmentManager : IDepartmentManager
    {
        private IDepartmentAccessor _departmentAccessor;
        private ICrewMemberAccessor _crewMemberAccessor;

        public DepartmentManager(IDepartmentAccessor departmentAccessor,
            ICrewMemberAccessor crewMemberAccessor)
        {
            _departmentAccessor = departmentAccessor;
            _crewMemberAccessor = crewMemberAccessor;
        }

        public DepartmentViewModel Get(int? id)
        {
            var departmentDto = id.HasValue ? _departmentAccessor.Get(id.Value) : new DepartmentDTO();
            var crewMemberDtos = id.HasValue ? _crewMemberAccessor.GetAllByDepartment(departmentDto.Id) : new List<CrewMemberDTO>();

            var vm = Mapper.Map<DepartmentViewModel>(departmentDto);

            if (crewMemberDtos != null && crewMemberDtos.Any())
            {
                vm.People = Mapper.Map<List<MovieRoleViewModel>>(crewMemberDtos);
            }

            return vm;
        }

        public DepartmentsViewModel GetAll()
        {
            var departmentDtos = _departmentAccessor.GetAll();
            var crewMemberDtos = _crewMemberAccessor.GetAll().OrderBy(x => x.Person.FirstName);

            var vms = Mapper.Map<List<DepartmentViewModel>>(departmentDtos);

            foreach (var vm in vms)
            {
                var people = crewMemberDtos.Where(x => x.DepartmentId == vm.Id);

                if (people != null && people.Any())
                {
                    vm.People = Mapper.Map<List<MovieRoleViewModel>>(people);
                }
            }

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
