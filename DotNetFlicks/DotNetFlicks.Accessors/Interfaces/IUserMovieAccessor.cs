using DotNetFlicks.Accessors.Models.DTO;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface IUserMovieAccessor
    {
        UserMovieDTO GetByMovieAndUser(int movieId, string userId);

        List<UserMovieDTO> GetAllByUser(string userId);

        UserMovieDTO Save(UserMovieDTO dto);

        List<UserMovieDTO> DeleteAllByMovie(int movieId);
    }
}
