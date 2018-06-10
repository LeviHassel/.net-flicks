using AutoMapper;
using DotNetFlicks.Accessors.Accessors.Base;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Accessors.Models.EF;
using DotNetFlicks.Accessors.Models.EF.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Accessors.Accessors
{
    public class UserMovieAccessor : EntityAccessor<Entity>, IUserMovieAccessor
    {
        public UserMovieAccessor(DotNetFlicksContext db) : base(db)
        {
        }

        public UserMovieDTO GetByMovieAndUser(int movieId, string userId)
        {
            var entity = _db.UserMovies
                .Include(x => x.Movie).ThenInclude(x => x.Cast).ThenInclude(x => x.Person)
                .Include(x => x.Movie).ThenInclude(x => x.Crew).ThenInclude(x => x.Person)
                .Include(x => x.Movie).ThenInclude(x => x.Crew).ThenInclude(x => x.Department)
                .Include(x => x.Movie).ThenInclude(x => x.Genres).ThenInclude(x => x.Genre)
                .SingleOrDefault(x => x.MovieId == movieId && x.UserId == userId);

            var dto = Mapper.Map<UserMovieDTO>(entity);

            return dto;
        }

        public List<UserMovieDTO> GetAllByUser(string userId)
        {
            var entities = _db.UserMovies
                .Include(x => x.Movie)
                .Where(x => x.UserId == userId)
                .ToList();

            //TODO: Filter out expired rentals

            var dtos = Mapper.Map<List<UserMovieDTO>>(entities);

            return dtos;
        }

        public UserMovieDTO Save(UserMovieDTO dto)
        {
            var entity = Mapper.Map<UserMovie>(dto);

            _db.UserMovies.Update(entity);
            _db.SaveChanges();

            dto = Mapper.Map<UserMovieDTO>(entity);

            return dto;
        }

        public List<UserMovieDTO> DeleteAllByMovie(int movieId)
        {
            var entities = _db.UserMovies.Where(x => x.MovieId == movieId).ToList();

            _db.UserMovies.RemoveRange(entities);
            _db.SaveChanges();

            var dtos = Mapper.Map<List<UserMovieDTO>>(entities);

            return dtos;
        }
    }
}
