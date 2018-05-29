using DotNetFlicks.Accessors.Models.DTO;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface IUserMovieAccessor
    {
        UserMovieDTO Get(int id);

        List<UserMovieDTO> GetAllByUser(string userId);

        UserMovieDTO Save(UserMovieDTO dto);
    }
}
