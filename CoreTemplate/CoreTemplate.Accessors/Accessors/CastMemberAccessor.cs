using AutoMapper;
using CoreTemplate.Accessors.Accessors.Base;
using CoreTemplate.Accessors.Database;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Accessors.Models.EF;
using CoreTemplate.Accessors.Models.EF.Base;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Accessors.Accessors
{
    public class CastMemberAccessor : EntityAccessor<Entity>, ICastMemberAccessor
    {
        public CastMemberAccessor(CoreTemplateContext db) : base(db)
        {
        }

        /// <summary>
        /// For the given Movie, create/update all CastMembers in list and delete all CastMembers not in list
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public List<CastMemberDTO> SaveAll(int movieId, List<CastMemberDTO> dtos)
        {
            //Create/update entries from cast list
            var entities = Mapper.Map<List<CastMember>>(dtos ?? new List<CastMemberDTO>());
            _db.CastMembers.UpdateRange(entities);

            //Delete existing entries not in cast list
            var entityIds = entities.Select(x => x.Id);
            var entitiesToRemove = _db.CastMembers.Where(x => x.MovieId == movieId && !entityIds.Contains(x.Id)).ToList();
            _db.CastMembers.RemoveRange(entitiesToRemove);

            _db.SaveChanges();

            dtos = Mapper.Map<List<CastMemberDTO>>(entities);

            return dtos;
        }

        public List<CastMemberDTO> DeleteAllByMovie(int movieId)
        {
            var entities = _db.CastMembers.Where(x => x.MovieId == movieId).ToList();

            _db.CastMembers.RemoveRange(entities);
            _db.SaveChanges();

            var dtos = Mapper.Map<List<CastMemberDTO>>(entities);

            return dtos;
        }
    }
}
