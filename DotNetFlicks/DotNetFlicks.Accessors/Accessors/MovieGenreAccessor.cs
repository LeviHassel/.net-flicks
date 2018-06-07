using AutoMapper;
using DotNetFlicks.Accessors.Accessors.Base;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Accessors.Models.EF;
using DotNetFlicks.Accessors.Models.EF.Base;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Accessors.Accessors
{
    public class MovieGenreAccessor : EntityAccessor<Entity>, IMovieGenreAccessor
    {
        public MovieGenreAccessor(DotNetFlicksContext db) : base(db)
        {
        }

        public List<MovieGenreDTO> SaveAll(int movieId, List<int> genreIds)
        {
            var entities = _db.MovieGenres.Where(x => x.MovieId == movieId).ToList();

            //Ensure genre list exists to avoid null value errors
            genreIds = genreIds ?? new List<int>();

            //Create new entries from genre list
            var newGenreIds = genreIds.Except(entities.Select(x => x.GenreId));
            var newEntities = newGenreIds.Select(x => new MovieGenre { MovieId = movieId, GenreId = x }).ToList();
            entities.AddRange(newEntities);
            _db.MovieGenres.AddRange(newEntities);

            //Delete existing entries not in genre list
            var entitiesToRemove = entities.Where(x => !genreIds.Contains(x.GenreId));
            entities = entities.Except(entitiesToRemove).ToList();
            _db.MovieGenres.RemoveRange(entitiesToRemove);

            _db.SaveChanges();

            var dtos = Mapper.Map<List<MovieGenreDTO>>(entities);

            return dtos;
        }

        public List<MovieGenreDTO> DeleteAllByMovie(int movieId)
        {
            var entities = _db.MovieGenres.Where(x => x.MovieId == movieId).ToList();

            _db.MovieGenres.RemoveRange(entities);
            _db.SaveChanges();

            var dtos = Mapper.Map<List<MovieGenreDTO>>(entities);

            return dtos;
        }
    }
}
