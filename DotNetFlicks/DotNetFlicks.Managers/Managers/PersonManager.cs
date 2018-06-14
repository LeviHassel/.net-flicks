using AutoMapper;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Common.Configuration;
using DotNetFlicks.Managers.Interfaces;
using DotNetFlicks.ViewModels.Person;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Managers.Managers
{
    public class PersonManager : IPersonManager
    {
        private IPersonAccessor _personAccessor;

        public PersonManager(IPersonAccessor personAccessor)
        {
            _personAccessor = personAccessor;
        }

        public PersonViewModel Get(int? id)
        {
            var dto = id.HasValue ? _personAccessor.Get(id.Value) : new PersonDTO();
            var vm = Mapper.Map<PersonViewModel>(dto);

            vm.Roles = vm.Roles.OrderBy(x => x.MovieYear).ThenBy(x => x.MovieName).ToList();

            return vm;
        }

        public PaginatedList<PersonViewModel> GetQuery(IndexQuery query)
        {
            var dtos = _personAccessor.GetQuery(query);

            var vms = Mapper.Map<List<PersonViewModel>>(dtos);

            foreach (var vm in vms)
            {
                vm.Roles = vm.Roles.OrderBy(x => x.MovieName).ToList();
            }

            var count = _personAccessor.GetCount(query.Search);

            var paginatedList = new PaginatedList<PersonViewModel>(vms, count, query.PageIndex, query.PageSize);

            return paginatedList;
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
