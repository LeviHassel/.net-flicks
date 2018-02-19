using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface IMoviePersonAccessor
    {
        List<MoviePersonDTO> GetAll();

        List<MoviePersonDTO> GetAllByMovie(int movieId);

        List<MoviePersonDTO> GetAllByPerson(int personId);

        List<MoviePersonDTO> GetAllByJob(int jobId);

        /// <summary>
        /// For the given Movie, create all new MoviePeople in list and delete all MoviePeople not in list
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="dtos"></param>
        /// <returns></returns>
        List<MoviePersonDTO> SaveAll(int movieId, List<MoviePersonDTO> dtos);

        List<MoviePersonDTO> DeleteAllByMovie(int movieId);
    }
}
