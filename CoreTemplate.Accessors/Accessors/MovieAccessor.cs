using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoreTemplate.Accessors.Accessors.Base;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Accessors.Models.EF;
using CoreTemplate.Accessors.Models.EF.Base;
using Microsoft.EntityFrameworkCore;

namespace CoreTemplate.Accessors.Accessors
{
    public class MovieAccessor : EntityAccessor<Entity>, IMovieAccessor
    {
        public MovieAccessor(CoreTemplateContext db) : base(db)
        {
        }

        public MovieDTO Get(int id)
        {
            var entity = _db.Movies
              .Single(x => x.Id == id);

            var dto = Mapper.Map<MovieDTO>(entity);

            return dto;
        }

        public List<MovieDTO> GetAll()
        {
            var entities = _db.Movies
              .AsNoTracking()
              .ToList();

            var dtos = Mapper.Map<List<MovieDTO>>(entities);

            return dtos;
        }

        public MovieDTO Save(MovieDTO dto)
        {
            try
            {
                var entity = Mapper.Map<Movie>(dto);

                if (dto.Id == 0)
                {
                    //added
                    _db.Movies.Add(entity);
                }
                else
                {
                    //modified
                    _db.Entry(entity).State = EntityState.Modified;
                }

                _db.SaveChanges();

                var returnDto = Mapper.Map<MovieDTO>(entity);

                return returnDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
