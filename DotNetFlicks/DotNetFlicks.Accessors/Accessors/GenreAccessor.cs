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
    public class GenreAccessor : EntityAccessor<Entity>, IGenreAccessor
    {
        public GenreAccessor(DotNetFlicksContext db) : base(db)
        {
        }

        public GenreDTO Get(int id)
        {
            var entity = _db.Genres
                .Include(x => x.Movies).ThenInclude(x => x.Movie)
                .Single(x => x.Id == id);

            var dto = Mapper.Map<GenreDTO>(entity);

            return dto;
        }

        public List<GenreDTO> GetAll()
        {
            var entities = _db.Genres
                .Include(x => x.Movies).ThenInclude(x => x.Movie)
                .ToList();

            var dtos = Mapper.Map<List<GenreDTO>>(entities);

            return dtos;
        }

        public List<GenreDTO> GetAllByRequest(DataTableRequest request)
        {
            var query = _db.Genres
                .AsNoTracking()
                .Include(x => x.Movies).ThenInclude(x => x.Movie)
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

                case "Movies_Asc":
                    query = query.OrderBy(x => x.Movies.Count());
                    break;

                case "Movies_Desc":
                    query = query.OrderByDescending(x => x.Movies.Count());
                    break;
            }

            var entities = query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtos = Mapper.Map<List<GenreDTO>>(entities);

            return dtos;
        }

        public int GetCount(string search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return _db.Genres.Where(x => x.Name.ToLower().Contains(search.ToLower())).Count();
            }
            else
            {
                return _db.Genres.Count();
            }
        }

        public GenreDTO Save(GenreDTO dto)
        {
            var entity = Mapper.Map<Genre>(dto);

            _db.Genres.Update(entity);
            _db.SaveChanges();

            dto = Mapper.Map<GenreDTO>(entity);

            return dto;
        }

        public GenreDTO Delete(int id)
        {
            var entity = _db.Genres.Single(x => x.Id == id);

            _db.Genres.Remove(entity);
            _db.SaveChanges();

            var dto = Mapper.Map<GenreDTO>(entity);

            return dto;
        }
    }
}
