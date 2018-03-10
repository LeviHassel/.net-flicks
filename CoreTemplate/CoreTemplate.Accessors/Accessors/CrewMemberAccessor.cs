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
    public class CrewMemberAccessor : EntityAccessor<Entity>, ICrewMemberAccessor
    {
        public CrewMemberAccessor(CoreTemplateContext db) : base(db)
        {
        }

        public List<CrewMemberDTO> GetAll()
        {
            var entities = _db.CrewMembers
                .Include(x => x.Movie)
                .Include(x => x.Person)
                .Include(x => x.Department)
                .ToList();

            var dtos = Mapper.Map<List<CrewMemberDTO>>(entities);

            return dtos;
        }

        public List<CrewMemberDTO> GetAllByMovie(int movieId)
        {
            var entities = _db.CrewMembers
                .Include(x => x.Movie)
                .Include(x => x.Person)
                .Include(x => x.Department)
                .Where(x => x.MovieId == movieId)
                .ToList();

            var dtos = Mapper.Map<List<CrewMemberDTO>>(entities);

            return dtos;
        }

        public List<CrewMemberDTO> GetAllByPerson(int personId)
        {
            var entities = _db.CrewMembers
                .Include(x => x.Movie)
                .Include(x => x.Person)
                .Include(x => x.Department)
                .Where(x => x.PersonId == personId)
                .ToList();

            var dtos = Mapper.Map<List<CrewMemberDTO>>(entities);

            return dtos;
        }

        public List<CrewMemberDTO> GetAllByDepartment(int departmentId)
        {
            var entities = _db.CrewMembers
                .Include(x => x.Movie)
                .Include(x => x.Person)
                .Include(x => x.Department)
                .Where(x => x.DepartmentId == departmentId)
                .ToList();

            var dtos = Mapper.Map<List<CrewMemberDTO>>(entities);

            return dtos;
        }

        /// <summary>
        /// For the given Movie, create/update all CrewMembers in list and delete all CrewMembers not in list
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public List<CrewMemberDTO> SaveAll(int movieId, List<CrewMemberDTO> dtos)
        {
            //Create/update entries from crew list
            var entities = Mapper.Map<List<CrewMember>>(dtos ?? new List<CrewMemberDTO>());
            _db.CrewMembers.UpdateRange(entities);

            //Delete existing entries not in crew list
            var entityIds = entities.Select(x => x.Id);
            var entitiesToRemove = _db.CrewMembers.Where(x => x.MovieId == movieId && !entityIds.Contains(x.Id)).ToList();
            _db.CrewMembers.RemoveRange(entitiesToRemove);

            _db.SaveChanges();

            dtos = Mapper.Map<List<CrewMemberDTO>>(entities);

            return dtos;
        }

        public List<CrewMemberDTO> DeleteAllByMovie(int movieId)
        {
            var entities = _db.CrewMembers.Where(x => x.MovieId == movieId).ToList();

            _db.CrewMembers.RemoveRange(entities);
            _db.SaveChanges();

            var dtos = Mapper.Map<List<CrewMemberDTO>>(entities);

            return dtos;
        }
    }
}
