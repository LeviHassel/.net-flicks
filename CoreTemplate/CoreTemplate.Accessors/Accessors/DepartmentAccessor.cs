using AutoMapper;
using CoreTemplate.Accessors.Accessors.Base;
using CoreTemplate.Accessors.Database;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Accessors.Models.EF;
using CoreTemplate.Accessors.Models.EF.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Accessors.Accessors
{
    public class DepartmentAccessor : EntityAccessor<Entity>, IDepartmentAccessor
    {
        public DepartmentAccessor(CoreTemplateContext db) : base(db)
        {
        }

        public DepartmentDTO Get(int id)
        {
            var entity = _db.Departments.Single(x => x.Id == id);

            var dto = Mapper.Map<DepartmentDTO>(entity);

            return dto;
        }

        public List<DepartmentDTO> GetAll()
        {
            var entities = _db.Departments.ToList();

            var dtos = Mapper.Map<List<DepartmentDTO>>(entities);

            return dtos;
        }

        public DepartmentDTO Save(DepartmentDTO dto)
        {
            var entity = Mapper.Map<Department>(dto);

            if (dto.Id == 0)
            {
                //Create new entry
                _db.Departments.Add(entity);
            }
            else
            {
                //Modify existing entry
                _db.Entry(entity).State = EntityState.Modified;
            }

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
