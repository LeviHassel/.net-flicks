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
    public class JobTitleAccessor : EntityAccessor<Entity>, IJobTitleAccessor
    {
        public JobTitleAccessor(CoreTemplateContext db) : base(db)
        {
        }

        public JobTitleDTO Get(int id)
        {
            var entity = _db.JobTitles.Single(x => x.Id == id);

            var dto = Mapper.Map<JobTitleDTO>(entity);

            return dto;
        }

        public List<JobTitleDTO> GetAll()
        {
            var entities = _db.JobTitles.ToList();

            var dtos = Mapper.Map<List<JobTitleDTO>>(entities);

            return dtos;
        }

        public JobTitleDTO Save(JobTitleDTO dto)
        {
            var entity = Mapper.Map<JobTitle>(dto);

            if (dto.Id == 0)
            {
                //Create new entry
                _db.JobTitles.Add(entity);
            }
            else
            {
                //Modify existing entry
                _db.Entry(entity).State = EntityState.Modified;
            }

            _db.SaveChanges();

            dto = Mapper.Map<JobTitleDTO>(entity);

            return dto;
        }

        public JobTitleDTO Delete(int id)
        {
            var entity = _db.JobTitles.Single(x => x.Id == id);

            _db.JobTitles.Remove(entity);
            _db.SaveChanges();

            var dto = Mapper.Map<JobTitleDTO>(entity);

            return dto;
        }
    }
}
