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
    public class MovieAccessor : EntityAccessor<Entity>, IMovieAccessor
    {
        public MovieAccessor(CoreTemplateContext db) : base(db)
        {
        }

        public MovieDTO Get(int id)
        {
            var entity = _db.Movies
                .Include(x => x.Genres)
                .Include(x => x.People)
                .Single(x => x.Id == id);

            var dto = Mapper.Map<MovieDTO>(entity);

            return dto;
        }

        public List<MovieDTO> GetAll()
        {
            var entities = _db.Movies
                .Include(x => x.Genres).ThenInclude(x => x.Genre)
                .Include(x => x.People).ThenInclude(x => x.Person)
                .Include(x => x.People).ThenInclude(x => x.Job)
                .ToList();

            var dtos = Mapper.Map<List<MovieDTO>>(entities);

            return dtos;
        }

        public MovieDTO Save(MovieDTO dto)
        {
            var entity = Mapper.Map<Movie>(dto);

            if (dto.Id == 0)
            {
                //Create new entry
                _db.Movies.Add(entity);
            }
            else
            {
                //Modify existing entry
                _db.Entry(entity).State = EntityState.Modified;
            }

            _db.SaveChanges();

            dto = Mapper.Map<MovieDTO>(entity);

            return dto;
        }

        public MovieDTO Delete(int id)
        {
            var entity = _db.Movies.Single(x => x.Id == id);

            _db.Movies.Remove(entity);
            _db.SaveChanges();

            var dto = Mapper.Map<MovieDTO>(entity);

            return dto;
        }
    }
}
