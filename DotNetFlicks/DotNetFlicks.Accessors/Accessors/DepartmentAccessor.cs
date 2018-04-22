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
    public class DepartmentAccessor : EntityAccessor<Entity>, IDepartmentAccessor
    {
        public DepartmentAccessor(DotNetFlicksContext db) : base(db)
        {
        }

        public DepartmentDTO Get(int id)
        {
            var entity = _db.Departments
                .Include(x => x.Roles).ThenInclude(x => x.Person)
                .Include(x => x.Roles).ThenInclude(x => x.Movie)
                .Single(x => x.Id == id);

            var dto = Mapper.Map<DepartmentDTO>(entity);

            return dto;
        }

        public List<DepartmentDTO> GetAll()
        {
            var entities = _db.Departments
                //.Include(x => x.Roles).ThenInclude(x => x.Person)
                //.Include(x => x.Roles).ThenInclude(x => x.Movie)
                .ToList();

            var dtos = Mapper.Map<List<DepartmentDTO>>(entities);

            return dtos;
        }

        public DepartmentDTO Save(DepartmentDTO dto)
        {
            var entity = Mapper.Map<Department>(dto);

            _db.Departments.Update(entity);
            _db.SaveChanges();

            dto = Mapper.Map<DepartmentDTO>(entity);

            return dto;
        }

        public DepartmentDTO Delete(int id)
        {
            var entity = _db.Departments.Single(x => x.Id == id);

            _db.Departments.Remove(entity);
            _db.SaveChanges();

            var dto = Mapper.Map<DepartmentDTO>(entity);

            return dto;
        }
    }
}
