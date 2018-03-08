using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Common.Helpers;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Department;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Managers.Managers
{
    public class DepartmentManager : IDepartmentManager
    {
        private IDepartmentAccessor _departmentAccessor;
        private IMoviePersonAccessor _moviePersonAccessor;

        public DepartmentManager(IDepartmentAccessor departmentAccessor,
            IMoviePersonAccessor moviePersonAccessor)
        {
            _departmentAccessor = departmentAccessor;
            _moviePersonAccessor = moviePersonAccessor;
        }

        public DepartmentViewModel Get(int? id)
        {
            var departmentDto = id.HasValue ? _departmentAccessor.Get(id.Value) : new DepartmentDTO();
            var moviePersonDtos = id.HasValue ? _moviePersonAccessor.GetAllByDepartment(departmentDto.Id) : new List<MoviePersonDTO>();

            var vm = Mapper.Map<DepartmentViewModel>(departmentDto);

            if (moviePersonDtos != null && moviePersonDtos.Any())
            {
                vm.PeopleCount = moviePersonDtos.Count();
                vm.PeopleTooltip = ListHelper.GetBulletedList(moviePersonDtos.Select(x => string.Format("{0} - {1}", x.Person.FullName, x.Movie.Name)).ToList());
            }

            return vm;
        }

        public DepartmentsViewModel GetAll()
        {
            var departmentDtos = _departmentAccessor.GetAll();
            var moviePersonDtos = _moviePersonAccessor.GetAll().OrderBy(x => x.Person.FirstName);

            var vms = Mapper.Map<List<DepartmentViewModel>>(departmentDtos);

            foreach (var vm in vms)
            {
                var people = moviePersonDtos.Where(x => x.DepartmentId == vm.Id);

                if (people != null && people.Any())
                {
                    vm.PeopleCount = people.Count();
                    vm.PeopleTooltip = ListHelper.GetTooltipList(people.Select(x => string.Format("{0} - {1}", x.Person.FullName, x.Movie.Name)).ToList());
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
