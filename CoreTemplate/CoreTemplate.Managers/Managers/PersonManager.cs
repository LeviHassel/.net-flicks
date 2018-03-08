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
        private IMoviePersonAccessor _moviePersonAccessor;
        private IPersonAccessor _personAccessor;

        public PersonManager(IMoviePersonAccessor moviePersonAccessor,
            IPersonAccessor personAccessor)
        {
            _moviePersonAccessor = moviePersonAccessor;
            _personAccessor = personAccessor;
        }

        public PersonViewModel Get(int? id)
        {
            var personDto = id.HasValue ? _personAccessor.Get(id.Value) : new PersonDTO();
            var moviePersonDtos = id.HasValue ? _moviePersonAccessor.GetAllByPerson(personDto.Id) : new List<MoviePersonDTO>();

            var vm = Mapper.Map<PersonViewModel>(personDto);

            if (moviePersonDtos != null && moviePersonDtos.Any())
            {
                vm.MoviesCount = moviePersonDtos.Count();
                vm.MoviesTooltip = ListHelper.GetBulletedList(moviePersonDtos.Select(x => string.Format("{0} ({1})", x.Movie.Name, x.Department.Name)).ToList());
            }

            return vm;
        }

        public PeopleViewModel GetAll()
        {
            var personDtos = _personAccessor.GetAll();
            var moviePersonDtos = _moviePersonAccessor.GetAll().OrderBy(x => x.Movie.Name);

            var vms = Mapper.Map<List<PersonViewModel>>(personDtos);

            foreach (var vm in vms)
            {
                vm.Age = AgeHelper.GetAge(vm.BirthDate, vm.DeathDate);

                var movies = moviePersonDtos.Where(x => x.PersonId == vm.Id);

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
