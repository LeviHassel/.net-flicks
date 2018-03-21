using DotNetFlicks.Accessors.Models.DTO;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface IMovieAccessor
    {
        MovieDTO Get(int id);

        List<MovieDTO> GetAll();

        MovieDTO Save(MovieDTO dto);

        MovieDTO Delete(int id);
    }
}
