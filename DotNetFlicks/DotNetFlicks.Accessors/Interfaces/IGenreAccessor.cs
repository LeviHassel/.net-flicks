using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Common.Models;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface IGenreAccessor
    {
        GenreDTO Get(int id);

        List<GenreDTO> GetAll();

        List<GenreDTO> GetAllByRequest(DataTableRequest query);

        int GetCount(string search = null);

        GenreDTO Save(GenreDTO dto);

        GenreDTO Delete(int id);
    }
}
