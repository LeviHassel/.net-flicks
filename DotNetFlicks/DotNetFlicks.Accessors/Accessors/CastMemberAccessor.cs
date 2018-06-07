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
    public class CastMemberAccessor : EntityAccessor<Entity>, ICastMemberAccessor
    {
        public CastMemberAccessor(DotNetFlicksContext db) : base(db)
        {
        }

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
