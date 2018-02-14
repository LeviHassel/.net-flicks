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
            //This sucks because it assumes people will only pass in dtos for ONE movie
            var entities = _db.MoviePeople.Where(x => x.MovieId == dtos.First().MovieId).ToList();
            /*
            //Ensure genre list exists to avoid null value errors
            genreIds = genreIds ?? new List<int>();

            //Create new entries from genre list
            var newGenreIds = genreIds.Except(entities.Select(x => x.GenreId));
            var newEntities = newGenreIds.Select(x => new MoviePerson { MovieId = movieId, GenreId = x }).ToList();
            entities.AddRange(newEntities);
            _db.MoviePeople.AddRange(newEntities);

            //Delete existing entries not in genre list
            var entitiesToRemove = entities.Where(x => !genreIds.Contains(x.GenreId));
            entities = entities.Except(entitiesToRemove).ToList();
            _db.MoviePeople.RemoveRange(entitiesToRemove);

            _db.SaveChanges();
            */
            var dtos2 = Mapper.Map<List<MoviePersonDTO>>(entities);

            return dtos2;
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
