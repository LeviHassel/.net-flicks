using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface IMovieAccessor
    {
        MovieDTO Save(MovieDTO entity);

        List<MovieDTO> GetAll();

        MovieDTO Get(int id);

        MovieDTO Delete(int id);
    }
}
