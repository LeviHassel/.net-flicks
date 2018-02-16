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
    public class MoviePersonAccessor : EntityAccessor<Entity>, IMoviePersonAccessor
    {
        public MoviePersonAccessor(CoreTemplateContext db) : base(db)
        {
        }

        public List<MoviePersonDTO> GetAllByMovie(int movieId)
        {
            var entities = _db.MoviePeople.Where(x => x.MovieId == movieId).ToList();

            var dtos = Mapper.Map<List<MoviePersonDTO>>(entities);

            return dtos;
        }

        public List<MoviePersonDTO> SaveAll(List<MoviePersonDTO> dtos)
        {
            //TODO: Improve names and structure

            //TODO: Make sure people will only pass in dtos for ONE movie by restricting it or leaving a comment
            var entities = _db.MoviePeople.Where(x => x.MovieId == dtos.First().MovieId).ToList();

            //Ensure DTO list exists to avoid null value errors
            dtos = dtos ?? new List<MoviePersonDTO>();

            //Create new entries from DTO list
            var newDtos = dtos.Where(x => !entities.Any(y => y.MovieId == x.MovieId && y.PersonId == x.PersonId && y.JobId == x.JobId));
            var newEntities = newDtos.Select(x => new MoviePerson { MovieId = x.MovieId, PersonId = x.PersonId, JobId = x.JobId }).ToList();
            entities.AddRange(newEntities);
            _db.MoviePeople.AddRange(newEntities);

            //Delete existing entries not in DTO list
            var entitiesToRemove = entities.Where(x => !dtos.Any(y => y.MovieId == x.MovieId && y.PersonId == x.PersonId && y.JobId == x.JobId));
            entities = entities.Except(entitiesToRemove).ToList();
            _db.MoviePeople.RemoveRange(entitiesToRemove);

            _db.SaveChanges();

            dtos = Mapper.Map<List<MoviePersonDTO>>(entities);

            return dtos;
        }

        public List<MoviePersonDTO> DeleteAllByMovie(int movieId)
        {
            var entities = _db.MoviePeople.Where(x => x.MovieId == movieId).ToList();

            _db.MoviePeople.RemoveRange(entities);
            _db.SaveChanges();

            var dtos = Mapper.Map<List<MoviePersonDTO>>(entities);

            return dtos;
        }
    }
}
