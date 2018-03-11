using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Common.Helpers;
using CoreTemplate.Engines.Interfaces;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Movie;
using CoreTemplate.ViewModels.Person;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Managers.Managers
{
    public class PersonManager : IPersonManager
    {
        private ICastMemberAccessor _castMemberAccessor;
        private ICrewMemberAccessor _crewMemberAccessor;
        private IPersonAccessor _personAccessor;
        private IPersonEngine _personEngine;

        public PersonManager(ICastMemberAccessor castMemberAccessor,
            ICrewMemberAccessor crewMemberAccessor,
            IPersonAccessor personAccessor,
            IPersonEngine personEngine)
        {
            _castMemberAccessor = castMemberAccessor;
            _crewMemberAccessor = crewMemberAccessor;
            _personAccessor = personAccessor;
            _personEngine = personEngine;
        }

        public PersonViewModel Get(int? id, bool includeMovies = false)
        {
            var personDto = id.HasValue ? _personAccessor.Get(id.Value) : new PersonDTO();
            var castMemberDtos = id.HasValue ? _castMemberAccessor.GetAllByPerson(personDto.Id) : new List<CastMemberDTO>();
            var crewMemberDtos = id.HasValue ? _crewMemberAccessor.GetAllByPerson(personDto.Id) : new List<CrewMemberDTO>();

            var vm = Mapper.Map<PersonViewModel>(personDto);
            vm.Age = DateHelper.GetAge(vm.BirthDate, vm.DeathDate);
            vm.MoviesCount = castMemberDtos.Count() + crewMemberDtos.Count();

            if (includeMovies)
            {
                vm.Movies = Mapper.Map<List<MovieViewModel>>(crewMemberDtos.Select(x => x.Movie));
            }
            else
            {
                vm.MoviesTooltip = _personEngine.GetMoviesTooltip(castMemberDtos, crewMemberDtos, true);
            }

            return vm;
        }

        public PeopleViewModel GetAll()
        {
            var personDtos = _personAccessor.GetAll();
            var castMemberDtos = _castMemberAccessor.GetAll();
            var crewMemberDtos = _crewMemberAccessor.GetAll();

            var vms = Mapper.Map<List<PersonViewModel>>(personDtos);

            foreach (var vm in vms)
            {
                var castRoles = castMemberDtos.Where(x => x.PersonId == vm.Id).ToList();
                var crewRoles = crewMemberDtos.Where(x => x.PersonId == vm.Id).ToList();

                vm.MoviesCount = castRoles.Count() + crewRoles.Count();
                vm.MoviesTooltip = _personEngine.GetMoviesTooltip(castRoles, crewRoles);
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
