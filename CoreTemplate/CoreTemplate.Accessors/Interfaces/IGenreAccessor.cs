using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface IGenreAccessor
    {
        GenreDTO Get(int id);

        List<GenreDTO> GetAll();

        GenreDTO Save(GenreDTO dto);

        GenreDTO Delete(int id);
    }
}
