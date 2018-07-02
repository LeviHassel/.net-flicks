using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Common.Models;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface IMovieAccessor
    {
        MovieDTO Get(int id);

        List<MovieDTO> GetAll();

        List<MovieDTO> GetAllByRequest(DataTableRequest query);

        int GetCount(string search = null);

        MovieDTO Save(MovieDTO dto);

        MovieDTO Delete(int id);
    }
}
