using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface ICastMemberAccessor
    {
        List<CastMemberDTO> GetAll();

        List<CastMemberDTO> GetAllByMovie(int movieId);

        List<CastMemberDTO> GetAllByPerson(int personId);

        /// <summary>
        /// For the given Movie, create/update all CastMembers in list and delete all CastMembers not in list
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        List<CastMemberDTO> SaveAll(int movieId, List<CastMemberDTO> dtos);

        List<CastMemberDTO> DeleteAllByMovie(int movieId);
    }
}
