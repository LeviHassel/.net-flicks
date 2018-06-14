using AutoMapper;
using DotNetFlicks.Accessors.Accessors.Base;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Accessors.Models.EF;
using DotNetFlicks.Accessors.Models.EF.Base;
using DotNetFlicks.Common.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<PersonDTO> GetQuery(IndexQuery query)
        {
            var entities = _db.People
                .AsNoTracking()
                .Include(x => x.CastRoles).ThenInclude(x => x.Movie)
                .Include(x => x.CrewRoles).ThenInclude(x => x.Movie)
                .Include(x => x.CrewRoles).ThenInclude(x => x.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
            {
                entities = entities.Where(x => x.Name.ToLower().Contains(query.Search.ToLower()));
            }

            switch (query.SortOrder)
            {
                case "name_desc":
                    entities = entities.OrderByDescending(x => x.Name);
                    break;
                case "Roles":
                    entities = entities.OrderBy(x => x.CrewRoles.Count() + x.CastRoles.Count());
                    break;
                case "roles_desc":
                    entities = entities.OrderByDescending(x => x.CrewRoles.Count() + x.CastRoles.Count());
                    break;
                default:
                    entities = entities.OrderBy(x => x.Name);
                    break;
            }

            //TODO: Improve names
            var items = entities
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var dtos = Mapper.Map<List<PersonDTO>>(items);

            return dtos;
        }

        //TODO: Move this into GetQuery?
        public List<PersonDTO> GetAllByName(string query)
        {
            var entities = _db.People
                .Where(x => x.Name.ToLower().Contains(query.ToLower()))
                .ToList();

            var dtos = Mapper.Map<List<PersonDTO>>(entities);

            return dtos;
        }

        public int GetCount(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return _db.People.Where(x => x.Name.ToLower().Contains(search.ToLower())).Count();
            }
            else
            {
                return _db.People.Count();
            }
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
