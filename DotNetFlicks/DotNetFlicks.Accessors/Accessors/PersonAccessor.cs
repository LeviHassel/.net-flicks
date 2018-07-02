using AutoMapper;
using DotNetFlicks.Accessors.Accessors.Base;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Accessors.Models.EF;
using DotNetFlicks.Accessors.Models.EF.Base;
using DotNetFlicks.Common.Models;
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

        public List<PersonDTO> GetAllByRequest(DataTableRequest request)
        {
            var query = _db.People
                .AsNoTracking()
                .Include(x => x.CastRoles).ThenInclude(x => x.Movie)
                .Include(x => x.CrewRoles).ThenInclude(x => x.Movie)
                .Include(x => x.CrewRoles).ThenInclude(x => x.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.Name.ToLower().Contains(request.Search.ToLower()));
            }

            switch (request.SortOrder)
            {
                case "Name_Asc":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "Name_Desc":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "Roles_Asc":
                    query = query.OrderBy(x => x.CrewRoles.Count() + x.CastRoles.Count());
                    break;

                case "Roles_Desc":
                    query = query.OrderByDescending(x => x.CrewRoles.Count() + x.CastRoles.Count());
                    break;
            }

            var entities = query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtos = Mapper.Map<List<PersonDTO>>(entities);

            return dtos;
        }

        public List<PersonDTO> GetAllByName(string query)
        {
            var entities = _db.People
                .Where(x => x.Name.ToLower().Contains(query.ToLower()))
                .ToList();

            var dtos = Mapper.Map<List<PersonDTO>>(entities);

            return dtos;
        }

        public int GetCount(string search = null)
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
