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

        public List<MoviePersonDTO> GetAll()
        {
            var entities = _db.MoviePeople
                .Include(x => x.Movie)
                .Include(x => x.Person)
                .Include(x => x.Job)
                .ToList();

            var dtos = Mapper.Map<List<MoviePersonDTO>>(entities);

            return dtos;
        }

        public List<MoviePersonDTO> GetAllByMovie(int movieId)
        {
            var entities = _db.MoviePeople
                .Include(x => x.Movie)
                .Include(x => x.Person)
                .Include(x => x.Job)
                .Where(x => x.MovieId == movieId)
                .ToList();

            var dtos = Mapper.Map<List<MoviePersonDTO>>(entities);

            return dtos;
        }

        public List<MoviePersonDTO> GetAllByPerson(int personId)
        {
            var entities = _db.MoviePeople
                .Include(x => x.Movie)
                .Include(x => x.Person)
                .Include(x => x.Job)
                .Where(x => x.PersonId == personId)
                .ToList();

            var dtos = Mapper.Map<List<MoviePersonDTO>>(entities);

            return dtos;
        }

        public List<MoviePersonDTO> GetAllByJob(int jobId)
        {
            var entities = _db.MoviePeople
                .Include(x => x.Movie)
                .Include(x => x.Person)
                .Include(x => x.Job)
                .Where(x => x.JobId == jobId)
                .ToList();

            var dtos = Mapper.Map<List<MoviePersonDTO>>(entities);

            return dtos;
        }

        /// <summary>
        /// For the given Movie, create all new MoviePeople in list and delete all MoviePeople not in list
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public List<MoviePersonDTO> SaveAll(int movieId, List<MoviePersonDTO> dtos)
        {
            var entities = _db.MoviePeople.Where(x => x.MovieId == movieId).ToList();

            //Ensure DTO list exists to avoid null value errors
            dtos = dtos ?? new List<MoviePersonDTO>();

            //Create new entries from DTO list
            var newDtos = dtos.Where(x => !entities.Any(y => y.MovieId == x.MovieId && y.PersonId == x.PersonId && y.JobId == x.JobId));
            var newEntities = Mapper.Map<List<MoviePerson>>(newDtos);
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
