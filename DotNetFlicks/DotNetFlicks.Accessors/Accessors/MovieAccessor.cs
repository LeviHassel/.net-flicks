using AutoMapper;
using DotNetFlicks.Accessors.Accessors.Base;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Interfaces;
using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Accessors.Models.EF;
using DotNetFlicks.Accessors.Models.EF.Base;
using DotNetFlicks.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Accessors.Accessors
{
    public class MovieAccessor : EntityAccessor<Entity>, IMovieAccessor
    {
        public MovieAccessor(DotNetFlicksContext db) : base(db)
        {
        }

        public MovieDTO Get(int id)
        {
            var entity = _db.Movies
                .Include(x => x.Cast).ThenInclude(x => x.Person)
                .Include(x => x.Crew).ThenInclude(x => x.Person)
                .Include(x => x.Crew).ThenInclude(x => x.Department)
                .Include(x => x.Genres).ThenInclude(x => x.Genre)
                .Single(x => x.Id == id);

            var dto = Mapper.Map<MovieDTO>(entity);

            return dto;
        }

        public List<MovieDTO> GetAll()
        {
            var entities = _db.Movies
                .Include(x => x.Genres).ThenInclude(x => x.Genre)
                .ToList();

            var dtos = Mapper.Map<List<MovieDTO>>(entities);

            return dtos;
        }

        public List<MovieDTO> GetAllByRequest(DataTableRequest request)
        {
            var query = _db.Movies
                .AsNoTracking()
                .Include(x => x.Genres).ThenInclude(x => x.Genre)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.Name.ToLower().Contains(request.Search.ToLower()));
            }

            switch (request.SortOrder)
            {
                case "Name_Asc":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "Name_Desc":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "Date_Asc":
                    query = query.OrderBy(x => x.ReleaseDate);
                    break;

                case "Date_Desc":
                    query = query.OrderByDescending(x => x.ReleaseDate);
                    break;

                case "Genres_Asc":
                    query = query.OrderBy(x => x.Genres.Count());
                    break;

                case "Genres_Desc":
                    query = query.OrderByDescending(x => x.Genres.Count());
                    break;
            }

            var entities = query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtos = Mapper.Map<List<MovieDTO>>(entities);

            return dtos;
        }

        public int GetCount(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return _db.Movies.Where(x => x.Name.ToLower().Contains(search.ToLower())).Count();
            }
            else
            {
                return _db.Movies.Count();
            }
        }

        public MovieDTO Save(MovieDTO dto)
        {
            var entity = Mapper.Map<Movie>(dto);

            _db.Movies.Update(entity);
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
