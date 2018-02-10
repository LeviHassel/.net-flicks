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
    public class JobAccessor : EntityAccessor<Entity>, IJobAccessor
    {
        public JobAccessor(CoreTemplateContext db) : base(db)
        {
        }

        public JobDTO Get(int id)
        {
            var entity = _db.Jobs.Single(x => x.Id == id);

            var dto = Mapper.Map<JobDTO>(entity);

            return dto;
        }

        public List<JobDTO> GetAll()
        {
            var entities = _db.Jobs.ToList();

            var dtos = Mapper.Map<List<JobDTO>>(entities);

            return dtos;
        }

        public JobDTO Save(JobDTO dto)
        {
            var entity = Mapper.Map<Job>(dto);

            if (dto.Id == 0)
            {
                //Create new entry
                _db.Jobs.Add(entity);
            }
            else
            {
                //Modify existing entry
                _db.Entry(entity).State = EntityState.Modified;
            }

            _db.SaveChanges();

            dto = Mapper.Map<JobDTO>(entity);

            return dto;
        }

        public JobDTO Delete(int id)
        {
            var entity = _db.Jobs.Single(x => x.Id == id);

            _db.Jobs.Remove(entity);
            _db.SaveChanges();

            var dto = Mapper.Map<JobDTO>(entity);

            return dto;
        }
    }
}
