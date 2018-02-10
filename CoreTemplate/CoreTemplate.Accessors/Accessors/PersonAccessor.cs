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
    public class PersonAccessor : EntityAccessor<Entity>, IPersonAccessor
    {
        public PersonAccessor(CoreTemplateContext db) : base(db)
        {
        }

        public PersonDTO Get(int id)
        {
            var entity = _db.People.Single(x => x.Id == id);

            var dto = Mapper.Map<PersonDTO>(entity);

            return dto;
        }

        public List<PersonDTO> GetAll()
        {
            var entities = _db.People.ToList();

            var dtos = Mapper.Map<List<PersonDTO>>(entities);

            return dtos;
        }

        public PersonDTO Save(PersonDTO dto)
        {
            var entity = Mapper.Map<Person>(dto);

            if (dto.Id == 0)
            {
                //Create new entry
                _db.People.Add(entity);
            }
            else
            {
                //Modify existing entry
                _db.Entry(entity).State = EntityState.Modified;
            }

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
