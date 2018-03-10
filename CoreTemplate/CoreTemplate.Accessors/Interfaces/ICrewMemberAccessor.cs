using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface ICrewMemberAccessor
    {
        List<CrewMemberDTO> GetAll();

        List<CrewMemberDTO> GetAllByMovie(int movieId);

        List<CrewMemberDTO> GetAllByPerson(int personId);

        List<CrewMemberDTO> GetAllByDepartment(int departmentId);

        /// <summary>
        /// For the given Movie, create all new CrewMembers in list and delete all CrewMembers not in list
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        List<CrewMemberDTO> SaveAll(int movieId, List<CrewMemberDTO> dtos);

        List<CrewMemberDTO> DeleteAllByMovie(int movieId);
    }
}
