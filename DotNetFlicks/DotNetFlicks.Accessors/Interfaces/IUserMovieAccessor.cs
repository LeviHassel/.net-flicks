using DotNetFlicks.Accessors.Models.DTO;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface IUserMovieAccessor
    {
        UserMovieDTO GetByMovieAndUser(int movieId, string userId);

        /// <summary>
        /// Get all rented and purchased movies for a user, filtering out expired rentals
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<UserMovieDTO> GetAllByUser(string userId);

        UserMovieDTO Save(UserMovieDTO dto);

        List<UserMovieDTO> DeleteAllByMovie(int movieId);
    }
}
