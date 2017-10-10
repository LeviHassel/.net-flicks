using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface IMovieAccessor
    {
        MovieDTO Get(int id);

        List<MovieDTO> GetAll();

        MovieDTO Save(MovieDTO dto);

        MovieDTO Delete(int id);
    }
}
