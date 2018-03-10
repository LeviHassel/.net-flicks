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
        /// For the given Movie, create all new CrewMembers in list and delete all CrewMembers not in list
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public List<CrewMemberDTO> SaveAll(int movieId, List<CrewMemberDTO> dtos)
        {
            var entities = _db.CrewMembers.Where(x => x.MovieId == movieId).ToList();

            //Ensure DTO list exists to avoid null value errors
            dtos = dtos ?? new List<CrewMemberDTO>();

            //Create new entries from DTO list
            var newDtos = dtos.Where(x => !entities.Any(y => y.MovieId == x.MovieId && y.PersonId == x.PersonId && y.DepartmentId == x.DepartmentId));
            var newEntities = Mapper.Map<List<CrewMember>>(newDtos);
            entities.AddRange(newEntities);
            _db.CrewMembers.AddRange(newEntities);

            //Delete existing entries not in DTO list
            var entitiesToRemove = entities.Where(x => !dtos.Any(y => y.MovieId == x.MovieId && y.PersonId == x.PersonId && y.DepartmentId == x.DepartmentId));
            entities = entities.Except(entitiesToRemove).ToList();
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
