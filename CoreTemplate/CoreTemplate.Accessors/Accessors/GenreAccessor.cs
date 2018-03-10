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
    public class GenreAccessor : EntityAccessor<Entity>, IGenreAccessor
    {
        public GenreAccessor(CoreTemplateContext db) : base(db)
        {
        }

        public GenreDTO Get(int id)
        {
            var entity = _db.Genres.Single(x => x.Id == id);

            var dto = Mapper.Map<GenreDTO>(entity);

            return dto;
        }

        public List<GenreDTO> GetAll()
        {
            var entities = _db.Genres.ToList();

            var dtos = Mapper.Map<List<GenreDTO>>(entities);

            return dtos;
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
