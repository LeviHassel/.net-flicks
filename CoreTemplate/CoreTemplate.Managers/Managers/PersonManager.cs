using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Common.Helpers;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Person;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Managers.Managers
{
    public class PersonManager : IPersonManager
    {
        private ICrewMemberAccessor _crewMemberAccessor;
        private IPersonAccessor _personAccessor;

        public PersonManager(ICrewMemberAccessor crewMemberAccessor,
            IPersonAccessor personAccessor)
        {
            _crewMemberAccessor = crewMemberAccessor;
            _personAccessor = personAccessor;
        }

        public PersonViewModel Get(int? id)
        {
            var personDto = id.HasValue ? _personAccessor.Get(id.Value) : new PersonDTO();
            var crewMemberDtos = id.HasValue ? _crewMemberAccessor.GetAllByPerson(personDto.Id) : new List<CrewMemberDTO>();

            var vm = Mapper.Map<PersonViewModel>(personDto);

            if (crewMemberDtos != null && crewMemberDtos.Any())
            {
                vm.MoviesCount = crewMemberDtos.Count();
                vm.MoviesTooltip = ListHelper.GetBulletedList(crewMemberDtos.Select(x => string.Format("{0} ({1})", x.Movie.Name, x.Department.Name)).ToList());
            }

            return vm;
        }

        public PeopleViewModel GetAll()
        {
            var personDtos = _personAccessor.GetAll();
            var crewMemberDtos = _crewMemberAccessor.GetAll().OrderBy(x => x.Movie.Name);

            var vms = Mapper.Map<List<PersonViewModel>>(personDtos);

            foreach (var vm in vms)
            {
                vm.Age = AgeHelper.GetAge(vm.BirthDate, vm.DeathDate);

                var movies = crewMemberDtos.Where(x => x.PersonId == vm.Id);

                if (movies != null && movies.Any())
                {
                    vm.MoviesCount = movies.Count();
                    vm.MoviesTooltip = ListHelper.GetTooltipList(movies.Select(x => string.Format("{0} ({1})", x.Movie.Name, x.Department.Name)).ToList());
                }
            }

            return new PeopleViewModel { People = vms };
        }

        public PersonViewModel Save(PersonViewModel vm)
        {
            var dto = Mapper.Map<PersonDTO>(vm);
            dto = _personAccessor.Save(dto);
            vm = Mapper.Map<PersonViewModel>(dto);

            return vm;
        }

        public PersonViewModel Delete(int id)
        {
            var dto = _personAccessor.Delete(id);
            var vm = Mapper.Map<PersonViewModel>(dto);

            return vm;
        }
    }
}
