using AutoMapper;
using DotNetFlicks.Accessors.Accessors.Base;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Accessors.Models.EF;
using DotNetFlicks.Accessors.Models.EF.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Accessors.Accessors
{
    public class PersonAccessor : EntityAccessor<Entity>, IPersonAccessor
    {
        public PersonAccessor(DotNetFlicksContext db) : base(db)
        {
        }

        public PersonDTO Get(int id)
        {
            var entity = _db.People
                .Include(x => x.CastRoles).ThenInclude(x => x.Movie)
                .Include(x => x.CrewRoles).ThenInclude(x => x.Movie)
                .Include(x => x.CrewRoles).ThenInclude(x => x.Department)
                .Single(x => x.Id == id);

            var dto = Mapper.Map<PersonDTO>(entity);

            return dto;
        }

        public List<PersonDTO> GetAll()
        {
            var entities = _db.People
                //.Include(x => x.CastRoles).ThenInclude(x => x.Movie)
                //.Include(x => x.CrewRoles).ThenInclude(x => x.Movie)
                //.Include(x => x.CrewRoles).ThenInclude(x => x.Department)
                .ToList();

            var dtos = Mapper.Map<List<PersonDTO>>(entities);

            return dtos;
        }

        public List<PersonDTO> GetByName(string query)
        {
            var entities = _db.People
                .Where(x => x.Name.ToLower().Contains(query.ToLower()))
                .ToList();

            var dtos = Mapper.Map<List<PersonDTO>>(entities);

            return dtos;
        }

        public int GetCount()
        {
            return _db.People.Count();
        }

        public PersonDTO Save(PersonDTO dto)
        {
            var entity = Mapper.Map<Person>(dto);

            _db.People.Update(entity);
            _db.SaveChanges();

            dto = Mapper.Map<PersonDTO>(entity);

            return dto;
        }

        public PersonDTO Delete(int id)
        {
            var entity = _db.People.Single(x => x.Id == id);

            _db.People.Remove(entity);
            _db.SaveChanges();

            var dto = Mapper.Map<PersonDTO>(entity);

            return dto;
        }
    }
}
