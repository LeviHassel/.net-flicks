using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Person;
using System.Collections.Generic;

namespace CoreTemplate.Managers.Managers
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

            return vm;
        }

        public PersonsViewModel GetAll()
        {
            var dtos = _personAccessor.GetAll();
            var vms = Mapper.Map<List<PersonViewModel>>(dtos);

            return new PersonsViewModel { Persons = vms };
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
