using DotNetFlicks.Accessors.Models.DTO;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface ICrewMemberAccessor
    {
        /// <summary>
        /// For the given Movie, create/update all CrewMembers in list and delete all CrewMembers not in list
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        List<CrewMemberDTO> SaveAll(int movieId, List<CrewMemberDTO> dtos);

        List<CrewMemberDTO> DeleteAllByMovie(int movieId);
    }
}
